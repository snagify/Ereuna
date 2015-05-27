// Load the SDK asynchronously - It also works if I put it in script tags. Weird.
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

var facebookModule = (function ($) {

    // Here we run a very simple test of the Graph API after login is
    // successful.  See statusChangeCallback() for when this call is made.
    var testApi = function() {
        console.log('Welcome!  Fetching your information.... ');
        window.FB.api('/me', function (response) {
            console.log('Successful login for: ' + response.name);
            document.getElementById('status').innerHTML = 'Thanks for logging in, ' + response.name + '!';
        });
    };

    var statusChangeCallback = function (response) {
        console.log('statusChangeCallback');
        console.log(response);
        if (response.status === 'connected') {
            testApi();
        } else if (response.status === 'not_authorized') {
            document.getElementById('status').innerHTML = 'Please log into this app.';
        } else {
            document.getElementById('status').innerHTML = 'Please log into Facebook.';
        }
    };

    var checkLoginState = function () {
        window.FB.getLoginStatus(function (response) {
            statusChangeCallback(response);
        });
    };

    var facebookInit = function () {
        window.FB.init({
            appId: '923459464377126',
            cookie: true,  // enable cookies to allow the server to access the session
            xfbml: true,  // parse social plugins on this page
            version: 'v2.3' // use version 2.2
        });
    };


    return {
        facebookInit: facebookInit,
        checkLoginState: checkLoginState
    };

})($);


window.fbAsyncInit = function () {
    facebookModule.facebookInit();
    facebookModule.checkLoginState();
};

