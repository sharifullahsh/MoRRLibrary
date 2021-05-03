
$(document).ready(function () {
    var employeeId = $("#employeeId").val(); 
    //Official magazine
    var borrowedOfficalMagazinesTable = null;
    if (employeeId) {
        borrowedOfficalMagazinesTable = $('#borrowedOfficalMagazinesTable').DataTable({
            "language":
            {
                "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
            },
            "processing": true,
            "serverSide": false,
            "orderMulti": false,
            "dom": '<"top"i>rt<"bottom"lp><"clear">',
            ajax: {
                url: "/Borrow/getBorrowedOfficialMagazines/"+ employeeId,

                "type": "POST",
                "datatype": "json"
            },
            //searching: false, paging: true, info: true,
            columns: [
                {
                    data: "Publisher"
                },
                {
                    data: "PublicationDate"
                },
                {
                    data: "DateEntered"
                },
                {
                    data: "SerialNumber"
                },
                {
                    data: "CabinetNumber"
                },
                {
                    data: "ShelfNumber"
                },
                {
                    data: "borrowId",
                    width: "15%",
                    render: function (data) {
                        return "<button title='تسلیم جریده دوباره به کتاب خان توسط کارمند' class='btn btn-sm btn-success js-return-o-magazine' data-o-magazine-id =" + data + "><i class='m-1 fas fa-fw fa-undo'></i>برگشت</button>";
                    }
                }
            ]
        });

    }
    var allOfficialMagazineTable = $('#allOfficialMagazineTable').DataTable({
        "language":
        {
            "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
        },
        "processing": true,
        "serverSide": true,
        "orderMulti": false,
        "dom": '<"top"i>rt<"bottom"lp><"clear">',
        ajax: {
            url: "/OfficialMagazine/getOfficialMagazines",

            "type": "POST",
            "datatype": "json"
        },
        //searching: false, paging: true, info: true,
        columns: [
            {
                data: "Publisher"
            },
            {
                data: "PublicationDate"
            },
            {
                data: "DateEntered"
            },
            {
                data: "SerialNumber"
            },
            {
                data: "CabinetNumber"
            },
            {
                data: "ShelfNumber"
            },
            {
                data: "Quantity"

            },
            {
                data: "AvailableQuantity"

            },
            {
                data: "Id",
                width: "15%",
                render: function (data) {
                    return "<button title='گرفتن جریده توسط کارمند برای مطالعه' class='btn btn-sm btn-success js-borrow-o-magazine' data-o-magazine-id=" + data + "><i class='m-1 fab fa-get-pocket'></i>گرفتن</button>";
                }
            }
        ]
    });
    $("#btnOMagazineSearch").on("click", function () {
        allOfficialMagazineTable.columns(0).search($('#txtSource').val().trim());
        allOfficialMagazineTable.columns(1).search($('#txtPublisher').val().trim());
        allOfficialMagazineTable.draw();
    });
    $("#btnOMagazineReset").click(function () {
        allOfficialMagazineTable.columns(0).search("");
        allOfficialMagazineTable.columns(1).search("");
        allOfficialMagazineTable.draw();
    });

    //Action handle for add and return book
    borrowedOfficalMagazinesTable.on("click", '.js-return-o-magazine', function () {
        var button = $(this);
        bootbox.confirm({
            title: "گرفتن جریده از کارمند",
            message: "ایا تسلیمی جریده از کارمند را تایید میکنید؟",
            buttons: {
                confirm: {
                    label: 'بلی',
                    className: 'btn-success mr-2'
                },
                cancel: {
                    label: 'نخیر',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: "/Borrow/ReturnOfficialMagazine/" + button.attr("data-o-magazine-id"),
                        method: "POST",
                        success: function (response) {
                            toastr.success(response.responseText);
                            borrowedOfficalMagazinesTable.row(button.parents("tr")).remove().draw();
                            allOfficialMagazineTable.ajax.reload();
                        },
                        error: function (response) {
                            toastr.error(response.responseText);
                        }
                    });
                }
            }
        });
    });

    allOfficialMagazineTable.on("click", '.js-borrow-o-magazine', function () {
        var button = $(this);

        bootbox.confirm({
            title: "تسلیم جریده به کارمند",
            message: "ایا میخواهید که جریده رابه کارمند برای مطالعه بدهید؟",
            buttons: {
                confirm: {
                    label: 'بلی',
                    className: 'btn-success mr-2'
                },
                cancel: {
                    label: 'نخیر',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: "/Borrow/BorrowOfficialMagazine/" + button.attr("data-o-magazine-id") + "?employeeId=" + employeeId,
                        method: "POST",
                        success: function (response) {
                            toastr.success(response.responseText);
                            allOfficialMagazineTable.ajax.reload();
                            borrowedOfficalMagazinesTable.ajax.reload();
                        }, error: function (response) {
                            toastr.error(response.responseText);
                        }
                    });
                }
            }
        });
    });

});
