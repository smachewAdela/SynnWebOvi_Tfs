$(document).ready(function ()
{
	$('.container').css('width', '100%').css('margin','auto').addClass('synnbggeneric');
	$('#siteNavbar').css('background-color', '#007BFF').css('width', '80%').css('margin','auto').addClass('synnbggradient')
	.css('top', '15px');
	$('#siteNavbar .container').css('background', 'transparent');
	$('.nav-link').css('font-family', 'Impact');
	$('section').css('min-height','100vh').css('width', '100%').css('margin','20px').addClass('synnbggeneric');
	$('.row').css('padding-top', '30px').css('width', '80%').css('margin','auto');
	$('.gridfooterrow a').css('color', '#E24242');
    $(".synn-textbox-with-label input").focus(function () { OnInputFocuses($(this).parent());});
	$(".synn-textbox-with-label input").blur(function () { OnInputFocusesOut($(this).parent());});
    $("i").css('left', '15px');
	





/* end of jquery	 */
});

function OnInputFocuses(sender) {
	sender.find("label").hide();
}
function OnInputFocusesOut(sender) {
	sender.find("label").show();
}