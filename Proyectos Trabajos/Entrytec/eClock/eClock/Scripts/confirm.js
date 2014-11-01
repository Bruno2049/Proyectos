/*
 * SimpleModal Confirm Modal Dialog
 * http://www.ericmmartin.com/projects/simplemodal/
 * http://code.google.com/p/simplemodal/
 *
 * Copyright (c) 2010 Eric Martin - http://ericmmartin.com
 *
 * Licensed under the MIT license:
 *   http://www.opensource.org/licenses/mit-license.php
 *
 * Revision: $Id: confirm.js 254 2010-07-23 05:14:44Z emartin24 $
 */

jQuery(function ($) {
	$('#confirm-dialog input.confirm, #confirm-dialog a.confirm').click(function (e) {
		e.preventDefault();

		// example of calling the confirm function
		// you must use a callback function to perform the "yes" action
		confirm("Continue to the SimpleModal Project page?", function () {
			window.location.href = 'http://www.ericmmartin.com/projects/simplemodal/';
		});
	});
});

function Confirmacion(mensaje, callback) {
    $('#Confirmacion').modal({
        closeHTML: "<a href='#' title='Cerrar' class='modal-close'>x</a>",
        position: ["20%", ],
        overlayId: 'Confirmacion-overlay',
        containerId: 'Confirmacion-container',
        onShow: function (dialog) {
            var modal = this;

            $('.Confirmacion_message', dialog.data[0]).append(mensaje);

            // if the user clicks "yes"
            $('.yes', dialog.data[0]).click(function () {
                // call the callback                
                if ($.isFunction(callback)) {
                    callback.apply();
                }
                // close the dialog
                modal.close(); // or $.modal.close();
            });
        }
    });
}
function Alerta(mensaje) {
    $('#Alerta').modal({
        closeHTML: "<a href='#' title='Cerrar' class='modal-close'>x</a>",
        position: ["20%", ],
        overlayId: 'Alerta-overlay',
        containerId: 'Alerta-container',
        onShow: function (dialog) {
            var modal = this;

            $('.Alerta_message', dialog.data[0]).append(mensaje);

            // if the user clicks "yes"
            $('.yes', dialog.data[0]).click(function () {
                // close the dialog
                modal.close(); // or $.modal.close();
            });
        }
    });
}