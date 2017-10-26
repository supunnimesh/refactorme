$(document).ready(function(){

    init();

    function init() {
        $('#submitBtn').click(function () {
            calculatePoints();
        });
    }

    function calculatePoints() {
        hideError();
        var form = $('#wilksPointForm');
        if (validateForm()) {
            $.ajax({
                url: $(form).attr('action'),
                type: 'POST',
                data: $(form).serialize(),
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                success: function (data, textStatus, jqXHR) {
                    if (data !== null && data.status === 'success') {
                        $('#wPoints').html(data.result);

                        //adding history items
                        if (data.history !== null) {
                            $('#historyList').empty();
                            $(data.history).each(function () {
                                $('#historyList').append('<li>' + this + '</li>');
                            });
                        }
                    } else {
                        showError(data.message);
                    }
                },
                error: function (err) {
                    showError(data.message);
                }
            });
        }
    }

    // I'm doing basic validation here. We can use Unobtrusive validation to validate the form
    function validateForm() {
        var bodyWeight = $('#BodyWeight').val();
        var liftedWeight = $('#LiftedWeight').val();

        if (bodyWeight === null || bodyWeight === '') {
            showError('Please enter value for Body Weight');
            return false;
        } else {
            if (bodyWeight <= 0 || bodyWeight > 500) {
                showError('Body weight shoule be between 1 and 500 KG');
                return false;
            }
        }

        if (liftedWeight === null || liftedWeight === '') {
            showError('Please enter value for Lifted Weight');
            return false;
        } else {
            if (liftedWeight <= 0 || liftedWeight > 1000) {
                showError('Lifted weight shoule be between 1 and 1000 KG');
                return false;
            }
        }

        return true;
    }

    function showError(message) {
        $('#error').removeClass('hide');
        $('#error').empty().append('<div class="alert alert-danger">' + message + '</div >');
    }

    function hideError() {
        $('#error').addClass('hide');
    }
});