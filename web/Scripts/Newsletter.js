
function UpdateStatus() {
    NewsLetterService.GetNewsLetterStatus(GetNewsLetterStatusCompleted);
}

function GetNewsLetterStatusCompleted(result) {

    var lNewsletterStatus = result;

    var percentage = lNewsletterStatus.PercentageCompleted;
    var sentMails = lNewsletterStatus.SentMails;
    var totalMails = lNewsletterStatus.TotalMails;
    var isSending = lNewsletterStatus.IsSending;

    if (totalMails < 0)
        totalMails = '???';

    var dIsSending = $get('dIsSending');
    var progBar = $get('progressbar');

    progBar.style.width = percentage + '%';
    dIsSending.innerHTML = '<b>' + percentage + '% Complete: '
        '<b><br/>' + sentMails + ' out of ' + totalMails + ' emails have been sent.';

    if (isSending) {
        dIsSending.innerHTML = dIsSending.innerHTML + '<br/>Currently sending a NewsLetter....';
        setTimeout(UpdateStatus, 5000);
    } else {
    dIsSending.innerHTML = dIsSending.innerHTML + '<br/>Not sending a NewsLetter....';
    }

}
