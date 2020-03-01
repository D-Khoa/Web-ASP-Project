var isModLogin = true;
//Register mod user
function Register() {
  //Get mail and password from login.html page
  const usermail = document.querySelector("#user_mail").value;
  const userpass = document.querySelector("#password").value;
  //Create user with email and password
  authRef
    .createUserWithEmailAndPassword(usermail, userpass)
    .then(function() {
      //Get user after register
      const currUser = authRef.currentUser;
      //Send a verify mail to email of user
      currUser
        .sendEmailVerification()
        .then(function() {
          //Add uid of user into firestore
          docRef.doc("mods/" + currUser.uid).set({
            email: currUser.email,
            btnactutors: "",
            btnsensors: ""
          });
          //Add uid of user into realtime DB
          dbRef.ref("mods/" + currUser.uid).set({
            email: currUser.email,
            btnactutors: "",
            btnsensors: ""
          });
          // Email sent.
          window.alert("Please check your mail to verify your account");
        })
        .catch(function(error) {
          // An error happened.
          var errorCode = error.code;
          var errorMessage = error.message;
          // ...
          window.alert(errorCode + ": " + errorMessage);
        });
    })
    .catch(function(error) {
      // Handle Errors here.
      var errorCode = error.code;
      var errorMessage = error.message;
      // ...
      window.alert(errorCode + ": " + errorMessage);
    });
}
//Login mod user
function Login() {
  //Get email and password
  const usermail = document.querySelector("#user_mail").value;
  const userpass = document.querySelector("#password").value;
  //Login with email and password
  authRef
    .signInWithEmailAndPassword(usermail, userpass)
    .then(function() {
      //Get current user
      const currUser = authRef.currentUser;
      //If user is verified
      if (currUser.emailVerified) {
        window.alert("Welcome " + usermail);
        //Send info of user to index.html page
        var queryString = "?para1=" + usermail + "&para2=" + currUser.uid;
        window.location.href = "index.html" + queryString;
      }
      //If user isn't verified then alert
      else window.alert("Please verify your account before log-in");
    })
    .catch(function(error) {
      // Handle Errors here.
      var errorCode = error.code;
      var errorMessage = error.message;
      window.alert(errorCode + ": " + errorMessage);
    });
}
//Add new member
function AddNewMember() {
  //Get current mod user
  const currUser = authRef.currentUser;
  //Get member name and passsword
  const membername = document.querySelector("#member_name").value;
  const memberpass = document.querySelector("#member_password").value;
  //Check username
  var isExist = false;
  docRef
    .doc("users/" + membername)
    .get()
    .then(function(doc) {
      //If user is exists then alert
      if (doc.exists) {
        window.alert(
          "Username: " + membername + " is exists! Please choose another name!"
        );
        isExist = true;
      }
    });
  //If user isn't exists then create a new
  if (!isExist) {
    docRef
      .doc("users/" + membername)
      .set({
        uid: currUser.uid,
        password: memberpass
      })
      .then(function() {
        window.alert("Add member: " + membername + " successful!");
      })
      .catch(function(error) {
        // An error happened.
        var errorCode = error.code;
        var errorMessage = error.message;
        window.alert(errorCode + ": " + errorMessage);
      });
  }
}
//Member login
function MemberLogin() {
  //Get username and password
  const inputname = document.querySelector("#member_name").value;
  const inputpass = document.querySelector("#member_password").value;
  dbRef
    .ref("users/" + inputname)
    .once("value")
    .then(function(snapshot) {
      //If username is exist
      if (snapshot.exists()) {
        //Check password
        console.log(snapshot.val().password);
        if (inputpass == snapshot.val().password) {
          window.alert("Welcome " + inputname);
          //Send user info to index.html page
          var queryString = "?para1=" + inputname + "&para2=" + snapshot.val().uid;
          window.location.href = "index.html" + queryString;
        } else window.alert("Wrong password!");
      } else window.alert("User is not exists!");
    })
    .catch(function(error) {
      // An error happened.
      var errorCode = error.code;
      var errorMessage = error.message;
      window.alert(errorCode + ": " + errorMessage);
    });
}
//Change mods login mode and members login mode
function ChangeLogin() {
  if (isModLogin) {
    isModLogin = false;
    document.getElementById("mem_login_form").style.display = "block";
    document.getElementById("login_form").style.display = "none";
  } else {
    isModLogin = true;
    document.getElementById("mem_login_form").style.display = "none";
    document.getElementById("login_form").style.display = "block";
  }
}
