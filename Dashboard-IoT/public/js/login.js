function Login() {
	var userEmail = document.getElementById("username").value;
	var userPass = document.getElementById("password").value;
	firebase.auth().signInWithEmailAndPassword(userEmail, userPass).then(function(){
		currUser = userEmail;
		window.alert("Welcome " + userEmail);
		document.getElementById("login_form").submit();
	}).catch(function(error) {
	  // Handle Errors here.
	  var errorCode = error.code;
	  var errorMessage = error.message;
	  window.alert(errorMessage);
	  // ...
	});
}

function Logout(){	firebase.auth().signOut().then(function() {	  	
	// Sign-out successful.
	currUser = null;
	window.location.href = "login.html";
	window.alert("Sign-out successful!")
}).catch(function(error) {
	// An error happened.
	window.alert(error.message);
});
}

function GetCurrentUser(){
	firebase.auth().onAuthStateChanged(function(user) {
		if (user) {
	    // User is signed in.
	    document.getElementById("user_name").innerHTML = user.email;
	} else {
	    // No user is signed in.
	    window.location.href = "login.html";
	}
});
}

function Register(){
	const iusername = document.querySelector("#username");
	const iemail = document.querySelector("#email");
	const ipassword = document.querySelector("#password");
	const docRef = firebase.firestore().doc("users/" + iusername.value);
	docRef.set({
		email : iemail.value,
		password : ipassword.value,
	}).then(function(){
		console.log("Register successful!");
	}).catch(function(error){
		console.log("Got an error: ", error);
	});
}