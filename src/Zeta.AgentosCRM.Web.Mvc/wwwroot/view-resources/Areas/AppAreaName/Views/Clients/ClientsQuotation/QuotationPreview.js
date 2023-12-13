(function () {
    $(function () {
        function printInvoiceAndRedirect() {
            // Create a hidden iframe
            var printFrame = document.createElement('iframe');
            printFrame.style.display = 'none';
            document.body.appendChild(printFrame);

            // Get the content to print
            var contentToPrint = document.getElementById('kt_app_content_container').innerHTML;

            // Write content and stylesheets to the iframe
            var printDocument = printFrame.contentDocument;
            printDocument.open();
            printDocument.write('<html><head><title>Print Invoice</title>');

            // Include stylesheets from the current window
            var stylesheets = document.styleSheets;
            for (var i = 0; i < stylesheets.length; i++) {
                printDocument.write('<link rel="stylesheet" type="text/css" href="' + stylesheets[i].href + '">');
            }

            printDocument.write('</head><body>');
            printDocument.write(contentToPrint);
            printDocument.write('</body></html>');
            printDocument.close();

            // Print the content in the same window
            printFrame.contentWindow.print();

            // Remove the iframe
            document.body.removeChild(printFrame);

            // Redirect after printing
            window.location = "/AppAreaName/Clients/ClientsQuotationPreview";
        }

        // Attach the click event to #saveBtn
        $('#buttonPrint').click(function () {
            // Call the printInvoiceAndRedirect function
            //printInvoiceAndRedirect();
            // Add the 'no-print' class before printing
            $('.app-header').addClass('hide-on-print');
            $('.app-footer').addClass('hide-on-print');
            $('.svg-icon').addClass('hide-on-print');

            // Trigger window print
            window.print();

            // Remove the 'no-print' class after printing
            $('.app-header').removeClass('hide-on-print');
            $('.app-footer').removeClass('hide-on-print');
            $('.svg-icon').removeClass('hide-on-print');
        });

        
    });
})(jQuery);