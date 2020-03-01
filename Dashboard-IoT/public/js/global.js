const authRef = firebase.auth();
const dbRef = firebase.database();
const docRef = firebase.firestore();
var display_current_user = "User Name";
var isLogin = false;
//Logout mod user
function Logout() {
  authRef
    .signOut()
    .then(function() {
      // Sign-out successful.
      isLogin = false;
      window.location.href = "login.html";
      window.alert("Log-out successful!");
    })
    .catch(function(error) {
      // An error happened.
      var errorCode = error.code;
      var errorMessage = error.message;
      window.alert(errorCode + ": " + errorMessage);
    });
}
/*Get user info
	function GetCurrentUser()
	{
	authRef.onAuthStateChanged(function(user) {
	if (user) {
	// User is signed in.
	//document.getElementById("user_name").innerHTML = user.email;
	display_current_user = user.email;
	document.body.style.display = "block";
	} else {
	// No user is signed in.
	document.body.style.display = "none";
	window.location.href = "login.html";			
	}
	});
}*/

function CheckLogin() {
  var queryString = decodeURIComponent(window.location.search);
  if (queryString == "") window.location.href = "login.html";
  queryString = queryString.substring(1);
  var queries = queryString.split("&");
  isLogin = queries[0].split("=")[1];
  display_current_user = queries[1].split("=")[1];
  if (isLogin) {
    document.getElementById("user_name").innerHTML = display_current_user;
  } else window.location.href = "login.html";
}

function LoginData(url) {
  var queryString = "?para1=" + isLogin + "&para2=" + display_current_user;
  window.location.href = url + queryString;
}
