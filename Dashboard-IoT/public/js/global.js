//Firebase SDK
const authRef = firebase.auth();
const dbRef = firebase.database();
const docRef = firebase.firestore();
//Default username
var display_current_user = "User Name";
//User id
var userID = "";

//Logout mod user
function Logout() {
  //Confirm user before log-out
  if (confirm("Do you want log-out?")) {
    authRef
      .signOut()
      .then(function() {
        // Sign-out successful.
        isLogin = false;
        window.location.href = "login.html";
        console.log("Log-out successful!");
      })
      .catch(function(error) {
        // An error happened.
        var errorCode = error.code;
        var errorMessage = error.message;
        console.log(errorCode + ": " + errorMessage);
      });
  }
}
//Check log-in state and user info
function CheckLogin() {
  //Decode URI
  var queryString = decodeURIComponent(window.location.search);
  //If URI not contains user info then return login.html page
  if (queryString == "") window.location.href = "login.html";
  //Get user info
  queryString = queryString.substring(1);
  var queries = queryString.split("&");
  display_current_user = queries[0].split("=")[1];
  userID = queries[1].split("=")[1];
  //Change display username
  document.getElementById("user_name").innerHTML = display_current_user;
}
//Send user info when change page
function LoginData(url) {
  var queryString = "?para1=" + display_current_user + "&para2=" + userID;
  window.location.href = url + queryString;
}
function GetData(fromVal) {
  return fromVal;
}
