firebase.auth().onAuthStateChanged(function(user) {
	if (user) {
	    // User is signed in.
	    document.getElementById("user_info_div").style.display = 'block';
	    document.getElementById("main_log_div").style.display = 'none';
	    document.getElementById("write_data").style.display = 'block';
	    var user = firebase.auth().currentUser;
	    if(user != null)
	    {
	    	var email_id = user.email;
	    	document.getElementById("user_para").innerHTML = "Welcome user: "+ email_id;
	    }
	} 
	else {
		// No user is signed in.
		var btnCallLogin = document.getElementById("top_login_button");
		btnCallLogin.onclick = function(){
			document.getElementById("main_log_div").style.display = 'block';
			document.getElementById("user_info_div").style.display = 'none';
			document.getElementById("write_data").style.display = 'none';
			document.body.style.background = 'rgba(0,0,0,0.5)';
		}
	}
});

function Register(){
	var userEmail = document.getElementById("login_email").value;
	var userPass = document.getElementById("login_pass").value;
	firebase.auth().createUserWithEmailAndPassword(userEmail, userPass).catch(function(error) {
	  // Handle Errors here.
	  var errorCode = error.code;
	  var errorMessage = error.message;
	  // ...
	  window.alert("Error: " + errorMessage);
	});
}

function Login() {
	var userEmail = document.getElementById("login_email").value;
	var userPass = document.getElementById("login_pass").value;
	firebase.auth().signInWithEmailAndPassword(userEmail, userPass).catch(function(error) {
	  // Handle Errors here.
	  var errorCode = error.code;
	  var errorMessage = error.message;
	  // ...
	  window.alert("Error: " + errorMessage);
	});
}

function Logout(){
	firebase.auth().signOut().then(function() {
	  // Sign-out successful.
	}).catch(function(error) {
	  // An error happened.
	});
}

// Your web app's Firebase configuration
var firebaseConfig = {
	apiKey: "AIzaSyAdjr0vxcb9ceC6FuvtgPSstlZDI6qkeAI",
	authDomain: "web-api-33bdf.firebaseapp.com",
	databaseURL: "https://web-api-33bdf.firebaseio.com",
	projectId: "web-api-33bdf",
	storageBucket: "web-api-33bdf.appspot.com",
	messagingSenderId: "28477694938",
	appId: "1:28477694938:web:964040f7057c3e123f56e3",
	measurementId: "G-HD3SFE36CH"
};
      // Initialize Firebase
      firebase.initializeApp(firebaseConfig);
      firebase.analytics();
      function writeUserData() {
      	var name = document.getElementById("write_name").value;
      	var email = document.getElementById("write_email").value;
      	var pass = document.getElementById("write_pass").value;

      	firebase.database().ref('users/' + name).set({
      		email: email,
      		password : pass
      	}).catch(function(error){
      		var errorMessage = error.message;
      		window.alert("Error: " + errorMessage);
      	});
      	window.alert("successful: " + name);
      }

      function readUserData(){
      	var userId = firebase.auth().currentUser.uid;
      	var name = document.getElementById("write_name").value;
      	var email = document.getElementById("write_email").value;
      	var pass = document.getElementById("write_pass").value;

      	return firebase.database().ref('/users/' + name).once('value').then(function(snapshot) {
      		email = (snapshot.val() && snapshot.val().email) || 'Empty';
      		pass = (snapshot.val() && snapshot.val().password) || 'Empty';
	  // ...
	  document.getElementById("write_email").value = email;
	  document.getElementById("write_pass").value = pass;
	});
      }