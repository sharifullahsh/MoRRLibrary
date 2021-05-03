$(document).ready(function () {
    isEmployee();
    $("#chkIsEmployee").change(function () {
        isEmployee();
    })
})
function isEmployee() {
    if ($("#chkIsEmployee").is(':checked')) {
        $(".ifEmployee").show();
        $("#IsEmployee").val(true);
    } 
    else {
        $("#IsEmployee").val(false);
        $(".ifEmployee").hide();
        $('#DepartmentId option:selected').val(0);
    }
}