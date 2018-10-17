$(document).ready(function ()
{
    $('.container').css('width', '100%').css('margin', 'auto').addClass('synnbggeneric');
	$('#siteNavbar').css('background-color', '#007BFF').css('width', '100%').css('margin','auto').addClass('synnbggradient')
	.css('top', '15px');
	$('#siteNavbar .container').css('background', 'transparent');
	$('.nav-link').css('font-family', 'Impact');
	$('section').css('min-height', '100vh').css('width', '100%').css('margin', '20px').addClass('synnbggeneric');
	$('.container').css('max-width', '');
	$('.row').css('width', '80%');
	$('.row').css('padding-top', '30px').css('width', '80%').css('margin','auto');
	$('.gridfooterrow a').css('color', '#E24242');
    $(".synn-textbox-with-label input").focus(function () { OnInputFocuses($(this).parent());});
	$(".synn-textbox-with-label input").blur(function () { OnInputFocusesOut($(this).parent());});
    $("i").css('left', '15px');
    $("#btnadddic").click(function () { AddToDictionary(); });
    $("#btnadddiary").click(function () { AddToDiary(); });
	$(".nav-link").click(function () {Nav($(this));});
	$("#dvaccordion").accordion();

	
	$('#ddlShopItems').multiselect({columns: 1,placeholder: 'בחר מוצר להוספה',search: true,selectAll: true});
	$(".ms-search input").attr("placeholder", "חיפוש");
	$(".ddltran").html("בחר הכל");
	$(".ms-options").css('overflow','').css("'overflow-x','hidden'").css("'overflow-y','auto'");
	$(".container").css('max-width','');
	
/* DialogAlert("Done Loading", "Test Header"); */



/* end of jquery	 */
});

function OnInputFocuses(sender) {
	sender.find("label").hide();
}
function OnInputFocusesOut(sender) {
	sender.find("label").show();
}
function DialogAlert(message, header)
{
	$("#dialog h3").empty().text(header);
  $("#dialog p").empty().text(message);
    $("#dialog").dialog();
}

function AddToDictionary()
{
	var key = $("#txadddickey").val();
	var value = $("#txadddicval").val();
		$("#txadddickey").val('');
	$("#txadddicval").val('');
	
	if(!key || key.length > 100)
	{
		DialogAlert("יש להזין את כל השדות", "הוספה");
		return;
	}
	if(!value || value.length > 100)
	{
		DialogAlert("יש להזין את כל השדות", "הוספה");
		return;
	}
	
	var outparams = ' {key:"'+key+'",value:"'+value+'"}';

	PerformAdd("AddToDictionary", outparams);
}

function AddToDiary() {

    var title = $("#txadddiaryname").val();
    var description = $("#txadddiarydesc").val();
    var date = $("#txadddiarydate").val();

    $("#txadddiaryname").val('');
    $("#txadddiarydesc").val('');
    $("#txadddiarydate").val('');

    if (!title || title.length > 100) {
        DialogAlert("יש להזין את כל השדות", "הוספה");
        return;
    }
    if (!description || description.length > 100) {
        DialogAlert("יש להזין את כל השדות", "הוספה");
        return;
    }
    if (!date || date.length > 100) {
        DialogAlert("יש להזין את כל השדות", "הוספה");
        return;
    }

    var outparams = ' {title:"' + title + '",description:"' + description + '",date:"' + date + '"}';

    PerformAdd("AddToCalendar", outparams);
}

function PerformAdd(methodname, outparams) {

	$.ajax({
		type: "POST",
		url: "Main.aspx/" + methodname,
		data: outparams,  
		dataType: 'Json',  
		contentType: "application/Json; charset=utf-8",
		success: OnAddSuccess,
		error: OnAddError
	});
}

function OnAddSuccess(data) {
	
	DialogAlert("פעולה בוצעה בהצלחה", "הוספה");
}


function OnAddError(xhr, ajaxOptions, thrownError) {
	  if(xhr.status==404) 
        alert(thrownError);
	DialogAlert("תקלה - פעולה לא בוצעה בהצלחה", "הוספה");
}
function Nav(destination)
{
    //var currentactive = $('nav').find('active').attr('name');
    //alert(currentactive);
    //$('nav').find('active').removeClass('active');
    //destination.addClass('active');
    //$('nav').find('a[href="#' + $(this).attr('id') + '"]').addClass('active');
    //alert(destination.attr('name'));
	/* window.location.replace(destination); */
}