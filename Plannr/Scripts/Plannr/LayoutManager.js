

(function () {

    var cache = {};
    var sel = $('#content');

    var showContent = function (data) {
        
        sel.html(data);
    };

    var clearCache = function () {
        cache = {};
    };


    $('#sidebar ul a').live('click', function (e) {

        var loc = $(this).attr('href');

        if (typeof cache[loc] !== 'undefined') {
            
            showContent(cache[loc]);
        } else {

            $.ajax({

                url: loc,
                success: function (data) {

                    cache[loc] = data;
                    
                    showContent(data);
                }
            })
        }

        e.preventDefault();
        e.stopPropagation();
        return false;


    });




})()