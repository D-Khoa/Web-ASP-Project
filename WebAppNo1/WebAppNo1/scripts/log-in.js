firebase.auth().onAuthStateChanged(function(user) {
	if (user) {
	    // User is signed in.
	    var user = firebase.auth().currentUser;
		if(user != null)
		{
			document.getElementById("user_para").innerHTML = "Welcome " + user.email + " has logged in.";
			document.getElementById("login_box").style.display = 'none';
			document.getElementById("top_bar").style.display = 'block';
		}		
	} 
	else {
				// No user is signed in.
			document.getElementById("user_para").innerHTML = "None";
			document.getElementById("login_box").style.display = 'block';
			document.getElementById("top_bar").style.display = 'none';
		}
});

function Register(){
	var userEmail = document.getElementById("user_email").value;
	var userPass = document.getElementById("user_password").value;
	firebase.auth().createUserWithEmailAndPassword(userEmail, userPass).catch(function(error) {
	  // Handle Errors here.
	  var errorCode = error.code;
	  var errorMessage = error.message;
	  // ...
	  window.alert("Error: " + errorMessage);
	});
}

function Login() {
	var userEmail = document.getElementById("user_email").value;
	var userPass = document.getElementById("user_password").value;
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
	  window.alert("Sign out successful.")
	}).catch(function(error) {
	  // An error happened.
	  window.alert(error.message);
	});
}