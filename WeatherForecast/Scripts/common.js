$(document).ready(function () {
    // Add smooth scrolling to all links in navbar + footer link
    $(".navbar a, footer a[href='#myPage']").on('click', function (event) {
        // Make sure this.hash has a value before overriding default behavior
        if (this.hash !== "") {
            // Prevent default anchor click behavior
            event.preventDefault();

            // Store hash
            var hash = this.hash;

            // Using jQuery's animate() method to add smooth page scroll
            // The optional number (900) specifies the number of milliseconds it takes to scroll to the specified area
            $('html, body').animate({
                scrollTop: $(hash).offset().top
            }, 900, function () {

                // Add hash (#) to URL when done scrolling (default click behavior)
                window.location.hash = hash;
            });
        } // End if
    });

    $(window).scroll(function () {
        $(".slideanim").each(function () {
            var pos = $(this).offset().top;

            var winTop = $(window).scrollTop();
            if (pos < winTop + 600) {
                $(this).addClass("slide");
            }
        });
    });
})

function GetAvailableHeight() {
    try {
        var containerHeight = $(document).height();

        if (document.body !== null && document.body !== undefined && document.body.scrollHeight > containerHeight)
            containerHeight = document.body.scrollHeight;

        var newHeight = containerHeight - 60;
        newHeight = document.documentElement.clientHeight;

        if ($(".navbar-static-top").length)
            newHeight = newHeight - 70;
        if ($(".main-footer").length)
            newHeight = newHeight - 70;

        return newHeight;
    }
    catch (err) {
        HandleError(err);
    }
}

function GetAvailableWidth() {
    try {
        var containerWidth = $(document).width();

        if (document.body !== null && document.body !== undefined && document.body.scrollWidth > containerWidth)
            containerWidth = document.body.scrollWidth;

        var newWidth = containerWidth - 60;
        newWidth = document.documentElement.clientWidth;

        if ($(".main-sidebar").length)
            newWidth = newWidth - 55;

        return newWidth;
    }
    catch (err) {
        HandleError(err);
    }
}