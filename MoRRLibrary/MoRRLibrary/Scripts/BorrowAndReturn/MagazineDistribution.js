$(document).ready(function () {
    var employeeId = $("#employeeId").val();
    //Magaine borrowing
    var magazinesDistributedTable = null;
    if (employeeId) {
        magazinesDistributedTable = $('#magazinesDistributedTable').DataTable({
            "language":
            {
                "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
            },
            "processing": true,
            "serverSide": false,
            "orderMulti": false,
            "dom": '<"top"i>rt<"bottom"lp><"clear">'
            ,
            ajax: {
                url: "/Borrow/getDistributedMagazine/" + employeeId,

                "type": "POST",
                "datatype": "json"
            },
            //searching: false, paging: true, info: true,
            columns: [
                {
                    data: "Name"
                },
                {
                    data: "MagazineType"
                },
                {
                    data: "Publisher"
                },
                {
                    data: "PublicationDate"
                },
                {
                    data: "SerialNumber"

                },
                {
                    data: "DateEntered"

                }
            ]
        });
    }
    var allMagazinesTable = null;
    if (employeeId) {
        allMagazinesTable = $('#allMagazinesTable').DataTable({
            "language":
            {
                "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
            },
            "processing": true,
            "serverSide": true,
            "orderMulti": false,
            "dom": '<"top"i>rt<"bottom"lp><"clear">',
            ajax: {
                url: "/Magazine/getMagazines",

                "type": "POST",
                "datatype": "json"
            },
            //searching: false, paging: true, info: true,
            columns: [
                {
                    data: "Name"
                },
                {
                    data: "MagazineType"
                },
                {
                    data: "Publisher"
                },
                {
                    data: "PublicationDate"
                },
                {
                    data: "SerialNumber"

                },
                {
                    data: "DateEntered"

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
                    width: "20%",
                    render: function (data) {
                        return "<button title='گرفتن کتاب توسط کارمند برای مطالعه' class='btn btn-sm btn-success m-1 js-distribute-magazine' data-magazine-id=" + data + "><i class='p-1 fab fa-get-pocket'></i>گرفتن</button>";
                    }
                }
            ]
        });
    
        $('#btnMagazineSearch').on("click", function () {
            allMagazinesTable.columns(0).search($('#txtMagazineName').val().trim());
            allMagazinesTable.columns(1).search($('#MagazineTypeId').val());
            allMagazinesTable.draw();
        });
        $("#btnMagazineReset").click(function () {
            allMagazinesTable.columns(0).search("");
            allMagazinesTable.columns(1).search("");
            allMagazinesTable.draw();
        });
    }
    //Action handle for add magazine
    allMagazinesTable.on("click", '.js-distribute-magazine', function () {
        var button = $(this);
        bootbox.confirm({
            title: "تسلیم مجله به عضو",
            message: "ایا میخواهید که مجله رابه عضو برای مطالعه بدهید؟",
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
                        url: "/Borrow/BorrowMagazine/" + button.attr("data-magazine-id") + "?employeeId=" + employeeId,
                        method: "POST",
                        success: function (response) {
                            toastr.success(response.responseText);
                            allMagazinesTable.ajax.reload();
                            magazinesDistributedTable.ajax.reload();
                        }, error: function (response) {
                            toastr.error(response.responseText);
                        }
                    });
                }
            }
        });
    });
    //End Magazine borrowing
});
