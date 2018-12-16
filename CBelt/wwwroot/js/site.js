//$(document).ready(function () {
//    console.log("Document Ready");

//    let UserId = ("#UserId").val();
//    let EventId = $("#EventId").val()
//    console.log("EventId" + EventId);
//    console.log("UserId" + UserId);
//    const data = { "UserId": UserId, "EventId": EventId };
//    console.log("Data" + data);
//    $('#Delete').click(function () {
//        $.ajax({
//            url: "/Join",
//            data: JSON.stringify(data),
//            type: 'POST',
//            dataType: 'json',
//            headers: {
//                RequestVerificationToken:
//                    $('input:hidden[name="__RequestVerificationToken"]').val()
//            },
//            contentType: "application/json; charset=utf-8",
//            success: function (resp) {
//                console.log("success")
//                console.log(resp);
//            },
//            failure: function (resp) {
//                console.log(resp);
//            }
//        });
//    });

//    $('#Join').click(function () {
//        $.ajax({
//            url: "/Join",
//            type: 'POST',
//            data: JSON.stringify(data),
//            dataType: 'json',
//            contentType: "application/json; charset=utf-8",
//            headers: {
//                RequestVerificationToken:
//                    $('input:hidden[name="__RequestVerificationToken"]').val()
//            },
//            success: function (resp) {
//                $("#join").replaceWith(`<button type="submit" id="un - join" class="submit btn btn - success action - button">Un-Join</button>`);
//                console.log("success")
//            }
//        });
//    });

//    $('#Leave').click(function () {
//        $.ajax({
//            url: "/Join",
//            type: 'POST',
//            data: JSON.stringify(data),
//            dataType: 'json',
//            contentType: "application/json; charset=utf-8",
//            headers: {
//                RequestVerificationToken:
//                    $('input:hidden[name="__RequestVerificationToken"]').val()
//            },
//            success: function (resp) {
//                $("#un-join").replaceWith(`<button type="submit" id="join" class="submit btn btn-success">Join</button>`);
//                console.log("success")
//            }
//        });
//    });

//});