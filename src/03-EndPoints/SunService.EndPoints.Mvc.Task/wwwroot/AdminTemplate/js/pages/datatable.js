var tableMain = $('#subcategory-table').DataTable({
    "columnDefs": [{
        "targets": [0,3],
        "orderable": false
    }],
    "aaSorting": [],
    "pageLength": 25,
    "drawCallback": function() {
        var topestStatus = $("#btn-check-all-toggle").prop("checked");
        $("table td input[type='checkbox']").each(function(){
            currentStatus = $(this).prop("checked");
            if(topestStatus != currentStatus){
                console.log("Reversed");
                $("#btn-check-all-toggle").prop("checked", currentStatus);
            }
        });

        Modiran.initiCkeck();
    },
});
var tableMain = $('#user-table').DataTable({
    "columnDefs": [{
        "targets": [0, 10],
        "orderable": false
    }],
    "aaSorting": [],
    "pageLength": 25,
    "drawCallback": function () {
        var topestStatus = $("#btn-check-all-toggle").prop("checked");
        $("table td input[type='checkbox']").each(function () {
            currentStatus = $(this).prop("checked");
            if (topestStatus != currentStatus) {
                console.log("Reversed");
                $("#btn-check-all-toggle").prop("checked", currentStatus);
            }
        });

        Modiran.initiCkeck();
    },
});

var tableMain = $('#category-table').DataTable({
    "columnDefs": [{
        "targets": [0, 3],
        "orderable": false
    }],
    "aaSorting": [],
    "pageLength": 25,
    "drawCallback": function () {
        var topestStatus = $("#btn-check-all-toggle").prop("checked");
        $("table td input[type='checkbox']").each(function () {
            currentStatus = $(this).prop("checked");
            if (topestStatus != currentStatus) {
                console.log("Reversed");
                $("#btn-check-all-toggle").prop("checked", currentStatus);
            }
        });

        Modiran.initiCkeck();
    },
});

var tableMain = $('#homeservice-table').DataTable({
    "columnDefs": [{
        "targets": [0, 6],
        "orderable": false
    }],
    "aaSorting": [],
    "pageLength": 25,
    "drawCallback": function () {
        var topestStatus = $("#btn-check-all-toggle").prop("checked");
        $("table td input[type='checkbox']").each(function () {
            currentStatus = $(this).prop("checked");
            if (topestStatus != currentStatus) {
                console.log("Reversed");
                $("#btn-check-all-toggle").prop("checked", currentStatus);
            }
        });

        Modiran.initiCkeck();
    },
});
$(window).on( 'resize', function () {
    $('#data-table').css("width", "100%");
} );
var tableMain = $('#order-table').DataTable({
    "columnDefs": [{
        "targets": [0, 7],
        "orderable": false
    }],
    "aaSorting": [],
    "pageLength": 25,
    "drawCallback": function () {
        var topestStatus = $("#btn-check-all-toggle").prop("checked");
        $("table td input[type='checkbox']").each(function () {
            currentStatus = $(this).prop("checked");
            if (topestStatus != currentStatus) {
                console.log("Reversed");
                $("#btn-check-all-toggle").prop("checked", currentStatus);
            }
        });

        Modiran.initiCkeck();
    },
});

var tableMain = $('#rating-table').DataTable({
    "columnDefs": [{
        "targets": [0, 8],
        "orderable": false
    }],
    "aaSorting": [],
    "pageLength": 25,
    "drawCallback": function () {
        var topestStatus = $("#btn-check-all-toggle").prop("checked");
        $("table td input[type='checkbox']").each(function () {
            currentStatus = $(this).prop("checked");
            if (topestStatus != currentStatus) {
                console.log("Reversed");
                $("#btn-check-all-toggle").prop("checked", currentStatus);
            }
        });

        Modiran.initiCkeck();
    },
});
// Checkboxes
$(document).on('ifChanged', 'input#btn-check-all-toggle', function (event) {
    var isChecked = $("#btn-check-all-toggle").prop("checked");
    if(isChecked){
        $("table td input[type='checkbox']").iCheck("check").iCheck("update");
    }else{
        $("table td input[type='checkbox']").iCheck("uncheck").iCheck("update");
    }
});