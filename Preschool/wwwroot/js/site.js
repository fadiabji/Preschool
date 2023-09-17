// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



function printBill(subscriptionId) {
    // Get the HTML content of the order summary
    var subTypeName = document.getElementById("subTypeName").innerHTML;
    var createdAt = document.getElementById("createdAt").innerHTML;
    var expiresAt = document.getElementById("expiresAt").innerHTML;
    var price = document.getElementById("price").innerHTML;
    var subStatus = document.getElementById("subStatus").innerHTML;

    // Create a new window for printing
    var printWindow = window.open('', '', 'height=600,width=800');

    // Write the order summary HTML to the print window
    printWindow.document.write('<html><head><title>Bill no:</title></head><body>' + subTypeName + '<br/>' + createdAt + '<br/>' + expiresAt + '<br/>' + price + '<br/>' + subStatus + '<br/>' + ' </body></html>');

    // Print the window
    printWindow.print();

    // Close the window
    printWindow.close();
}
