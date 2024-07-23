$(window).ready(function () {
    siteEvents.bindLogoutEvents();
    siteEvents.bindMenuEvents();
    siteEvents.bindSearchEvents();
    siteEvents.bindNotificationIconEvents();
    sitePageLoadActions.checkNotifications();

    if ($(".message").html() != "") $(".message").show();
    $(".message").click(function () { $(this).fadeOut(); });

    if (!siteEvents.placeholderIsSupported) {
        $("header, footer").css({ "position": "initial" });
    }
    $("input[type=text]").focusout(function () {
        $(this).val($(this).val().trim());
    });
});

var sitePageLoadActions = {
    checkNotifications: function () {
        $.ajax({
            type: 'GET',
            url: '/Notifications/CheckNotifications',
            success: function (result) {
                if (result > 0) {
                    $(".hasNotifications").html(result);
                    $(".hasNotifications").show();
                }
            }
        });
    },

    checkFollowing: function () {
        $.ajax({
            type: 'GET',
            url: '/Notifications/CheckFollowing',
            data: {selectedUserId: $(".follow").attr("id")},
            success: function (result) {
                if (result.toLowerCase() == "true") {
                    $(".follow").toggleClass("greenBackground");
                    $(".follow").toggleClass("blueBackground");
                    $(".follow").html("Following");
                }
                $(".follow").css({ "display": "inline-block" });
            }
        });
    }
}

var siteEvents = {
    bindFollowEvents: function() {
        $(".follow").click(function () {
            var currentElement = $(this);
            var id = currentElement.attr("id");
            $.ajax({
                type: 'POST',
                url: '/Notifications/FollowUser',
                data: {selectedUserId: id},
                success: function (result) {
                    if (result.toLowerCase() == "true") {
                        currentElement.toggleClass("greenBackground");
                        currentElement.toggleClass("blueBackground");
                        if (currentElement.hasClass("greenBackground"))
                            currentElement.html("follow");
                        else
                            currentElement.html("Following");
                    }
                },
                error: function() {
                    $(".popupMessage").text("Something went wrong. Please try again later");
                }
            });
        });
    },

    bindNotificationIconEvents: function() {
        $(".notifications").click(function () {
            $("#notificationsContainer").toggleClass("show");
            if (!$("#notificationsContainer").hasClass("show")) return;

            $.ajax({
                type: 'GET',
                url: '/Notifications/GetNotifications',
                dataType: "html",
                success: function (html) {
                    $("#notificationsContainer").html(html);
                    if (html) {
                        $.ajax({
                            type: 'GET',
                            url: '/Notifications/MarkNotificationsAsSeen',
                            dataType: "application/html",
                            success: function (result) {
                                if (result.toLowerCase() == "true") {
                                    $(".hasNotifications").hide();
                                }
                            }
                         });
                    }
                },
                error: function() {
                    $(".notificationsContainer").html("<p>Something went wrong. Please try again later.</p>")
                }
            });
        });
    },

    bindFeedEvents: function () {

        $(".postActions li").unbind("click");
        $(".postActions li").bind("click", function () {
            var element = $(this);
            var id = element.attr("id");
            var action = element.attr("action")

            if (action == "" || action == null)
                return;

            $.ajax({
                type: 'POST',
                url: '/Feeds/AddLikeOrInterest',
                data: { feedId: id, feedNotificationType: action, text: "" },
                success: function (result) {
                    if (result) {
                        if (action == "2") {
                            element.removeAttr("action");
                        }
                        element.toggleClass("liked_1");
                        element.toggleClass("liked_0");
                    }
                }
            });
            return false;
        });
    },

    bindFollowChannelEvents: function () {
        var channelsToFollow = $("#channelsToFollow").val();
        var channelsToUnfollow = $("#channelsToUnFollow").val();

        $(".channelOption").unbind("click");
        $(".channelOption").bind("click", function () {
            var item = $(this);
            if (!item.hasClass("selectedOption")) {
                if (channelsToFollow.indexOf(item.attr("id")) == -1)
                channelsToFollow += item.attr("id") + ",";
                $("#channelsToFollow").val(channelsToFollow);
                item.html("Following");
            }
            else {
                if (channelsToUnfollow.indexOf(item.attr("id")) == - 1)
                    channelsToUnfollow += item.attr("id") + ",";
                $("#channelsToUnFollow").val(channelsToUnfollow);

                channelsToFollow = removeValue(channelsToFollow, item.attr("id"), ",");
                $("#channelsToFollow").val(channelsToFollow);
                item.html("Follow");
            }
            item.toggleClass("selectedOption")
        });
    },

    bindLogoutEvents: function () {

        $(".logout").click(function () {
            $("#logoutContainer").show();
        });
        $(".closeModal").click(function () {
            $("#logoutContainer").hide();
            return false;
        });
    },

    bindSearchEvents: function () {

        $(".search").click(function () {
            $("#actionBarSearch").fadeIn();
            $("#searchText").focus();
        });
        $(".btnClose").click(function () {
            $("#actionBarSearch").fadeOut();
            $("#searchText").val("");
            return false;
        });
        $("#actionBarSearch").keyup(function (e) {
            if (e.keyCode == 27) {
                $("#actionBarSearch").fadeOut();
                $("#searchText").val("");
                return false;
            }
        });
    },

    bindMenuEvents: function () {
        $("#bars").click(function () {
            $("#sideMenu").toggleClass("show");
        });
    },

    bindEditQualificaionEvents: function (canEdit) {
        if (!canEdit)
            return;
        $(".qualificationListItem").unbind("click");
        $(".qualificationListItem").click(function () {
            var currentParent = $(this).find(".qualificationInfo");
            $("#editQualificationItemContainer").appendTo(currentParent);
            $("#editQualificationItemContainer").attr("selectedid", currentParent.attr("id"));
            $("#editQualificationItemContainer").attr("qualtype", currentParent.attr("qualType"));
            $("#editQualificationItemContainer").stop().fadeIn();
        });

        $("#editQualificationItemContainer, .qualificationListItem").unbind("mouseleave");
        $("#editQualificationItemContainer, .qualificationListItem").mouseleave(function () {
            $("#editQualificationItemContainer").attr("selectedid", "");
            $("#editQualificationItemContainer").attr("qualtype", "");
            $("#editQualificationItemContainer").stop().fadeOut();
        });

        $("#closeEditQualificationOption").unbind("click");
        $("#closeEditQualificationOption").click(function () {
            window.setTimeout(function () { 
                $("#editQualificationItemContainer").attr("selectedid", "");
                $("#editQualificationItemContainer").attr("qualtype", "");
                $("#editQualificationItemContainer").stop().fadeOut();
            }, 50);
        });
    },

    placeholderIsSupported: function () {
        var test = document.createElement('input');
        return ('placeholder' in test) && !navigator.userAgent.match(/Opera Mini/i);
    },
}

var removeValue = function (list, value, separator) {
    separator = separator || ",";
    var values = list.split(separator);
    for (var i = 0 ; i < values.length ; i++) {
        if (values[i] == value) {
            values.splice(i, 1);
            return values.join(separator);
        }
    }
    return list;
}