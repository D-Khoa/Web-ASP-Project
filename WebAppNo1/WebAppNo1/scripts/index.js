firebase.auth().onAuthStateChanged(function(user) {
	if (user) {
	    // User is signed in.
	    document.getElementById("user_div").style.display = 'block';
	    document.getElementById("login_div").style.display = 'none';
	    var user = firebase.auth().currentUser;
	    if(user != null)
	    {
	    	var email_id = user.email;
	    	document.getElementById("user_para").innerHTML = "Welcome user: "+ email_id;
	    }
	} 
	else {
	    // No user is signed in.
	    document.getElementById("user_div").style.display = 'none';
	    document.getElementById("login_div").style.display = 'block';
	}
});

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