using Microsoft.AspNetCore.Mvc;
using refactorme.ViewModels;
using refactorme.Extensions;
using System;
using System.Collections.Generic;

namespace refactorme.Controllers
{
    public class CalculateController : Controller
    {
        private const string calculationHistorySessionkey = "calculationHistory";

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CalculatePoints(CalculateWilksModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var coeff = Wilks.CalculateWilks(model.BodyWeight, model.Gender == utils.Enums.Gender.Male);
                    var totalPoints = Math.Round(coeff * model.LiftedWeight, 2);

                    var history = HttpContext.Session.GetObject<List<string>>(calculationHistorySessionkey);
                    if (history == null)
                    {
                        history = new List<string>();
                    }

                    var genderString = model.Gender == utils.Enums.Gender.Female ? "F" : "M";
                    history.Add($"({genderString}) {model.LiftedWeight}KG @{model.BodyWeight}KG BW = {totalPoints}");
                    HttpContext.Session.SetObject(calculationHistorySessionkey, history);

                    return Json(new { Result = totalPoints, Status = "success", History = history });
                }
                catch (Exception e)
                {
                    return Json(new { Status = "error", Message = "Oops, something went wrong, please try that again." });
                }
            }

            return Json(new { Status = "error", Message = "Oops, something went wrong, please try that again." });
        }
    }
}