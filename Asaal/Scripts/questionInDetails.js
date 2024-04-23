$(document).ready(function () {

    // get words from resourses files
    var addReplay = $("#addReplay").attr("data-addReplay"),
        replayTxt = $("#replay").attr("data-replay"),
        deleteWarningMsg = $("#deleteWarningMsg").attr("data-deleteWarningMsg"),
        sorryMsgText = $("#sorryMsgText").attr("data-sorryMsgText");


    // add new answer
    $("#answerForm").on("submit", function (e) {
        e.preventDefault();

        var answerEl = $("#AnswerDto_Content"),
            answerContent = answerEl.val(),
            idOfQuestion = $("#Question_Id").val();

        if (answerEl.hasClass("valid")) {
            $.ajax({
                url: '/api/answers/',
                method: 'post',
                data: {
                    content: answerContent,
                    questionId: idOfQuestion
                },
                success: function (answer) {
                    answer.PublishDate = new Date().toLocaleString();

                    $(".answers .answersHeading").after(`
                                            <div class="answer" data-id="${answer.Id}" id="${answer.Id}">
                                                <div class="row">
                                                    <div class="col-sm-7">
                                                        <h4 class="fullName">
                                                            <img src="${answer.UserProfileImageSrc}" alt="" class="img-circle" />
                                                            <a href="/account/userProfile/${answer.UserId}">${answer.UserFullName}</a>
                                                        </h4>
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <div class="dateAndDelete">
                                                            <span class="date">${answer.PublishDate}</span>
                                                            <i class="glyphicon glyphicon-trash deleteBtn" data-elementId="${answer.Id}" data-targetElement="answer"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="answerBody">
                                                    <div class="vote text-center">
                                                        <i class="glyphicon glyphicon-menu-up voteBtn" data-job="true"></i>
                                                        <span>0</span>
                                                        <i class="glyphicon glyphicon-menu-down voteBtn" data-job="false"></i>
                                                    </div>
                                                    <div class="content">
                                                        <p>
                                                            ${answer.Content}
                                                        </p>
                                                    </div>
                                                </div>

                                                <div class="replaysControls row">
                                                    <div class="col-sm-6">
                                                        <h5 class="addReplay">${addReplay}</h5>
                                                    </div>
                                                    <div class="col-sm-6 text-right">
                                                        <h5 class="showReplays">0 ${replayTxt}</h5>
                                                    </div>
                                                </div>

                                                <div class="replays">
                                                    <p class="sorryMsg">
                                                        ${sorryMsgText}
                                                    </p>
                                                </div>

                                            </div>
                                        `);

                    answerEl.val("");
                    $(".answers > .sorryMsg").slideUp();

                },
                error: function (e) {
                    console.log("error is:- " + e);
                }
            });
        }
    });


    // add new replay
    $("#replayForm").on("submit", function (e) {
        e.preventDefault();

        var replayEl = $("#ReplayDto_Content"),
            replayContent = replayEl.val(),
            idOfanswer = $(this).parents(".answer").attr("data-id"),
            targetanswer = $(this).parents(".answer");

        if (replayEl.hasClass("valid")) {
            $.ajax({
                url: '/api/replays/',
                method: 'post',
                data: {
                    content: replayContent,
                    answerId: idOfanswer
                },
                success: function (replay) {
                    replay.PublishDate = new Date().toLocaleString();

                    targetanswer.find(".replays").append(`
                                            <div class="answer replay" data-id="${replay.Id}">
                                                <div class="row">
                                                    <div class="col-sm-7">
                                                        <h4 class="fullName">
                                                            <img src="${replay.UserProfileImageSrc}" alt="Sorry" class="img-circle" />
                                                            <a href="/account/userProfile/${replay.UserId}">${replay.UserFullName}</a>
                                                        </h4>
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <div class="dateAndDelete">
                                                            <span class="date">${replay.PublishDate}</span>
                                                            <i class="glyphicon glyphicon-trash deleteBtn" data-elementId="${replay.Id}" data-targetElement="replay"></i>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="answerBody">
                                                    <div class="content">
                                                        <p>
                                                            ${replay.Content}
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>
                                        `);

                    targetanswer.find(".replays").slideDown();
                    targetanswer.find(".replays .sorryMsg").slideUp();
                    replayEl.val("");
                },
                error: function (e) {
                    console.log("error is:- " + e);
                }
            });
        }
    });


    // create new vote 
    $(document).on("click", ".voteBtn", function () {

        positiveOrNegative = $(this).attr("data-job");
        answer_id = $(this).parents(".answer").attr("data-id");
        votesCountContainer = $(this).siblings("span");

        var votesCount = parseInt(votesCountContainer.text()),
            youCantVoteMsg = $("#youCantVoteMsg").attr("data-youCantVoteMsg"),
            userOwnTheAnswerMsg = $("#userOwnTheAnswerMsg").attr("data-userOwnTheAnswerMsg"),
            youCantVoteAgainMsg = $("#youCantVoteAgainMsg").attr("data-youCantVoteAgainMsg");

        $.ajax({
            url: "/api/votes/",
            method: "post",
            data: {
                answerId: answer_id,
                isGoodAnswerOrNot: positiveOrNegative
            },
            success: function (data) {

                // user not authanticated so he cant vote
                if (data.Message === "Authorization has been denied for this request.") {
                    bootbox.alert(youCantVoteMsg);

                // user vote before on this answer so he cant vote again
                } else if (data === "userVoteBefore") {
                    bootbox.alert(youCantVoteAgainMsg);
                }

                // user tryng to vote his answer
                else if (data === "ownTheAnswer") {
                    bootbox.alert(userOwnTheAnswerMsg);
                }
                else {
                        if (positiveOrNegative === "true") {
                            votesCountContainer.text(votesCount + 1);
                            votesCountContainer.siblings(".voteBtn[data-job='true']").addClass("active");
                        }

                        else if (positiveOrNegative === "false") {
                            votesCountContainer.text(votesCount - 1);
                            votesCountContainer.siblings(".voteBtn[data-job='false']").addClass("active");
                        }
                }

            },
            error: function (e) {
                console.log(e);
            }
        });
    });


    // add replay form to first answer
    $(".answers .answer:first-of-type").each(function () {
        $(this).find(".replaysControls").after($("#replayForm"));
        $("#replayForm").removeClass("hidden");
    });

    // show all replays related with answer claint had perssed
    $(document).on("click", ".showReplays", function () {
        $(this).parents(".answer").find(".replays").slideToggle("fast");
    });

    // get replay form to pressed answer
    $(document).on("click", ".addReplay", function () {
        $(this).parents(".answer").find(".replaysControls").after($("#replayForm"));
    });


    // thank U Bootbox cause you make 'complex 58 line' to 'easy 32 line'
    // delete elements { question, answer, replay }
    $(document).on("click", ".deleteBtn", function () {
        var targetElement = $(this).attr("data-targetElement"),
            id = $(this).attr("data-elementId");

        bootbox.confirm(deleteWarningMsg, function (result) {
            if (result === true) {
                if (targetElement === "question") {
                    // send ajax request to remove answer
                    $("#questionDeleteForm").submit();
                }
                else if (targetElement === "answer") {
                    // send ajax request to remove answer
                    $.ajax({
                        method: "delete",
                        url: "/api/answers/" + id,
                        success: function () {
                            $(`.answer[data-id=${id}]`).fadeOut("fast").remove();
                        }
                    });
                }
                else if (targetElement === "replay") {
                    // send ajax request to remove replay
                    $.ajax({
                        method: "delete",
                        url: "/api/replays/" + id,
                        success: function () {
                            $(`.replay[data-id=${id}]`).fadeOut("fast").remove();
                        }
                    });
                }
            }
        });
    });

});