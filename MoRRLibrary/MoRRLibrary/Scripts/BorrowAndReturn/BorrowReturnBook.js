
$(document).ready(function () {
    var employeeId = $("#employeeId").val();
    //Book borrowing
    var borrowedBooksTable = null;
    if (employeeId) {
        borrowedBooksTable = $('#borrowedBooksTable').DataTable({
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
                url: "/Borrow/getEmployeeBorrowedBooks/" + employeeId,

                "type": "POST",
                "datatype": "json"
            },
            //searching: false, paging: true, info: true,
            columns: [
                {
                    data: "bookName"
                },
                {
                    data: "author"
                },
                {
                    data: "language"
                },
                {
                    data: "cabinetNumber"
                },
                {
                    data: "shelfNumber"
                },
                {
                    data: "serialNumber"

                },
                {
                    data: "borrowId",
                    width: "15%",
                    render: function (data) {
                        return "<button title='تسلیم کتاب دوباره به کتاب خان توسط کارمند' class='btn btn-sm btn-success js-return-book' data-borrow-id=" + data + "><i class='ml-1 fas fa-fw fa-undo'></i>برگشت</button>";
                    }
                }
            ]
        });
    }
    var allBooksTable = null;
    if (employeeId) {
        allBooksTable = $('#allBooksTable').DataTable({
            "language":
            {
                "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
            },
            "processing": true,
            "serverSide": true,
            "orderMulti": false,
            "dom": '<"top"i>rt<"bottom"lp><"clear">',
            ajax: {
                url: "/Book/getBooks",

                "type": "POST",
                "datatype": "json"
            },
            //searching: false, paging: true, info: true,
            columns: [
                {
                    data: "Name"
                },
                {
                    data: "Author"
                },
                {
                    data: "Language"
                },
                {
                    data: "CabinetNumber"
                },
                {
                    data: "ShelfNumber"
                },
                {
                    data: "SerialNumber"

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
                        return "<button title='گرفتن کتاب توسط کارمند برای مطالعه' class='btn btn-sm btn-success js-borrow-book' data-book-id=" + data + "><i class='ml-1 fab fa-get-pocket'></i>گرفتن</button>";
                    }
                }
            ]
        });
        $('#btnBookSearch').on("click", function () {
            allBooksTable.columns(0).search($('#txtBookName').val().trim());
            allBooksTable.columns(1).search($('#txtBookAuthorName').val().trim());
            allBooksTable.draw();
        });
        $("#btnBookReset").click(function () {
            allBooksTable.columns(0).search("");
            allBooksTable.columns(1).search("");
            allBooksTable.draw();
        });
    }
    //Action handle for add and return book
    borrowedBooksTable.on("click", '.js-return-book', function () {
        var button = $(this);
        bootbox.confirm({
            title: "گرفتن کتاب از عضو",
            message: "ایا تسلیمی کتاب از عضو را تایید میکنید؟",
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
                        url: "/Borrow/ReturnBook/" + button.attr("data-borrow-id"),
                        method: "POST",
                        success: function (response) {
                            toastr.success(response.responseText);
                            borrowedBooksTable.row(button.parents("tr")).remove().draw();
                            allBooksTable.ajax.reload();
                        },
                        error: function (response) {
                            toastr.error(response.responseText);
                        }
                    });
                }
            }
        });



    });
    allBooksTable.on("click", '.js-borrow-book', function () {
        var button = $(this);
        bootbox.confirm({
            title: "تسلیم کتاب به عضو",
            message: "ایا میخواهید که کتاب رابه عضو برای مطالعه بدهید؟",
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
                        url: "/Borrow/BorrowBook/" + button.attr("data-book-id") + "?employeeId=" + employeeId,
                        method: "POST",
                        success: function (response) {
                            toastr.success(response.responseText);
                            allBooksTable.ajax.reload();
                            borrowedBooksTable.ajax.reload();
                        }, error: function (response) {
                            toastr.error(response.responseText);
                        }
                    });
                }
            }
        });
    });
    //End book borrowing
});
