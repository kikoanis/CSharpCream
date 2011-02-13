/*
 * SimpleModal Confirm Modal Dialog
 * http://www.ericmmartin.com/projects/simplemodal/
 * http://code.google.com/p/simplemodal/
 *
 * Copyright (c) 2008 Eric Martin - http://ericmmartin.com
 *
 * Licensed under the MIT license:
 *   http://www.opensource.org/licenses/mit-license.php
 *
 * Revision: $Id: confirm.js 170 2008-12-04 19:03:12Z emartin24 $
 *
 */

//$(document).ready(function() {
//    $('#confirmDialog input.confirm, #confirmDialog a.confirm').click(function(e) {
//        e.preventDefault();

//        // example of calling the confirm function
//        // you must use a callback function to perform the "yes" action
//        var answer = confirm("Are you sure to delete this professor information?");//, function() {
//            //window.location.href = 'http://www.ericmmartin.com/projects/simplemodal/';
//            return answer;
//        //});
//    });
//});

function confirm(message, callback) {
    var answer = false;
    $('#confirm').modal({
        close: false,
        position: ["30%", ],
        overlayId: 'confirmModalOverlay',
        containerId: 'confirmModalContainer',
        onShow: function(dialog) {
            dialog.data.find('.message').append(message);

            // if the user clicks "yes"
            dialog.data.find('.yes').click(function() {
                // call the callback
                if ($.isFunction(callback)) {
                 callback.apply();
                }
                // close the dialog
                $.modal.close();
            });
        }
    });
}