$(document).ready(function () {
  $('.pDateTimePicker').MdPersianDateTimePicker({
            targetTextSelector: 'pDateTimePicker',
            dateFormat: 'yyyy-MM-dd',
            isGregorian: false,
            enableTimePicker: false,
            disableBeforeToday: true,
            
        });
});