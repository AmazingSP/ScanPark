$(function () {
    $('.js-basic-example').DataTable({
        "bLengthChange": false,
        "pageLength": 5,
        "language": {
            "loadingRecords": "Daten werden geladen - Bitte warten ...",
            "processing": "Daten werden verarbeitet",
            "search": "Suche: ",
		    "zeroRecords": "Keine Daten vorhanden",
		    "infoEmpty": "Keine Daten vorhanden",
		    "emptyTable": "Keine Daten vorhanden",
		    "info": "Seite _PAGE_ von _PAGES_",
		    "paginate" : {
		        "previous": "Zur&uuml;ck",
		        "next": "Vor",
		        "first": "Erste Seite",
		        "last": "Letzte Seite",
		    }

        }
	});
});