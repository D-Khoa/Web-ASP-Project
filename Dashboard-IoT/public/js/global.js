//Firebase SDK
const authRef = firebase.auth();
const dbRef = firebase.database();
const docRef = firebase.firestore();
const dbURL = "https://web-api-33bdf.firebaseio.com/";
//Default username
var display_current_user = "User Name";
//User id
var userID = "";
//Mod
var isMod = false;

//Logout mod user
function Logout() {
  //Confirm user before log-out
  if (confirm("Do you want log-out?")) {
    authRef
      .signOut()
      .then(function () {
        // Sign-out successful.
        console.log("Log-out successful!");
        window.location.href = "login.html";
      })
      .catch(function (error) {
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
  isMod = (queries[2].split("=")[1] == "true");
  //Change display username
  document.getElementById("user_name").innerHTML = display_current_user;
  //If mod then show device and member
  const allModElement = document.querySelectorAll(".mod-user");
  if (isMod) {
    for (let i = 0; i < allModElement.length; i++) {
      allModElement[i].style.display = "block";
    }
  }
  else {
    for (let i = 0; i < allModElement.length; i++) {
      allModElement[i].style.display = "none";
    }
  }
  /*var pageName = location.pathname.split("/").pop();
  switch (pageName) {
    case "dashboard":
      document.querySelector("#index_menu").style.background = "#578EBE";
      break;
    case "devices":
      document.querySelector("#device_menu").style.background = "#578EBE";
      break;
    case "members":
      document.querySelector("#member_menu").style.background = "#578EBE";
      break;
  }*/
}

function CheckMenu(sectionID) {
  window.location.href = sectionID;
  const dashboard = document.getElementById("dashboard");
  const devices = document.getElementById("devices");
  const members = document.getElementById("members");
  const dashboardmenu = document.getElementById("index_menu");
  const devicesmenu = document.getElementById("device_menu");
  const membersmenu = document.getElementById("member_menu");
  dashboard.style.display = "none";
  devices.style.display = "none";
  members.style.display = "none";
  dashboardmenu.style.background = "#1C2B36";
  devicesmenu.style.background = "#1C2B36";
  membersmenu.style.background = "#1C2B36";
  switch (sectionID) {
    case "#dashboard":
      dashboardmenu.style.background = "#578EBE";
      dashboard.style.display = "block";
      break;
    case "#devices":
      devicesmenu.style.background = "#578EBE";
      devices.style.display = "block";
      break;
    case "#members":
      membersmenu.style.background = "#578EBE";
      members.style.display = "block";
      break;
  }
}
//Send user info when change page
function LoginData(url) {
  var queryString = "?para1=" + display_current_user + "&para2=" + userID + "&para3=" + isMod;
  window.location.href = url + queryString;
}
