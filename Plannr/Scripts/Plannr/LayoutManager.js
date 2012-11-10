var cache;

(function () {

    cache = {};
    var sel = $('#content');

    var showContent = function (data,el) {
        hideLoading();
        $('#sidebar li.active').removeClass('active');
        $(el).parent().addClass('active');

        sel.html(data);
    };

    var clearCache = function () {
        cache = {};
    };

    var showLoading = function () {

        $('#overlay').fadeIn();
        $('#spinner').fadeIn();

    };

    var hideLoading = function () {
        $('#overlay').fadeOut();
        $('#spinner').fadeOut();
    };


    $('#sidebar ul a').live('click', function (e) {
        showLoading();
        var that = this;
        var loc = $(this).attr('href');
       
        if (typeof cache[loc] !== 'undefined') {
            
            showContent(cache[loc]);
        } else {

            $.ajax({

                url: loc,
                success: function (data) {

                    cache[loc] = data;
                    
                    showContent(data,that);
                }
            })
        }

        e.preventDefault();
        e.stopPropagation();
        return false;


    });




})()