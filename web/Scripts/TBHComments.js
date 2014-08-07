var dThinking = $get('dThinking');
var dCommentPosted = $get('dCommentPosted');

if (dCommentPosted != null) {
    HideElement('dCommentPosted');
    HideElement('dThinking');
}

function SetPage() {

    if ($get('dResults') != null) {
        HideElement('dResults');
    }

}

function StoreComment() {

    HideElement('dMakeComment');
    ShowElement('dThinking');

    var ArticleId = $get('ArticleId');
    var eMail = $get('txtEmail');
    var Title = $get('txtTitle');
    var Comment = $get('txtComment');
    var URL = $get('txtURL');
    var UserIP = $get('hUserIP');

    if (eMail.value.length > 0 && Comment.value.length > 0 && Title.value.length > 0) {

        var objCat = new BBICMS.BLL.Articles.Comment();
        objCat.ArticleId = ArticleId.value;
        objCat.addedByEmail = eMail.value;
        objCat.Title = Title.value
        objCat.Body = Comment.value;
        objCat.CommenterURL = URL.value;
        objCat.AddedByIP = UserIP.value;
        objCat.AddedBy = eMail.value;
        objCat.UpdatedBy = eMail.value;

        //   alert('calling the Service.');

        CommentService.PostComment(objCat, StoreCommentCompleteEvent, StoreCommentErrorEvent);

        // alert('Called the Service.');        

    } else {
        alert('Just not a valid form.');
    }

    return false;
}


function StoreCommentCompleteEvent(result, context) {

    HideElement('dThinking');
    ShowElement('dCommentPosted');

}

function StoreCommentErrorEvent(result, context) {
    alert('It blew up!');
    if (null != result) {
        alert(result.get_stackTrace());
    }
}

function ShowElement(ElementName) {
    $get(ElementName).style.display = '';
}

function ShowBlockElement(ElementName) {
    var a = $get(ElementName);
    a.style.display = 'block';
    a.style.overflow = 'auto';
    a.style.height = 'auto';
}

function HideElement(ElementName) {
    $get(ElementName).style.display = 'none';
}