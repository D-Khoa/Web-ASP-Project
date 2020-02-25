const authRef = firebase.auth();
const docRef = firebase.firestore();

function Register(){
	const usermail = document.querySelector("#user_mail").value;
	const userpass = document.querySelector("#password").value;
	authRef.createUserWithEmailAndPassword(usermail, userpass).then(function(){
		var currUser = authRef.currentUser;
		currUser.sendEmailVerification().then(function() {
			docRef.doc("mods/"+currUser.uid).set({
				email: currUser.email,
				btnactutors:"",
				btnsensors:"",
			});
			// Email sent.
			window.alert("Please check your mail to verify your account");
			}).catch(function(error) {
			// An error happened.
			var errorCode = error.code;
			var errorMessage = error.message;
			// ...
			window.alert(errorCode +": "+ errorMessage);
		});
		}).catch(function(error) {
		// Handle Errors here.
		var errorCode = error.code;
		var errorMessage = error.message;
		// ...
		window.alert(errorCode +": "+ errorMessage);
	});
}

function Login() {
	const usermail = document.querySelector("#user_mail").value;
	const userpass = document.querySelector("#password").value;
	authRef.signInWithEmailAndPassword(usermail, userpass).then(function(){
		var currUser = authRef.currentUser;
		if(currUser.emailVerified){
			window.alert("Welcome " + usermail);
			document.getElementById("login_form").submit();
		}
		else window.alert("Please verify your account before log-in");
		}).catch(function(error) {
		// Handle Errors here.
		var errorCode = error.code;
		var errorMessage = error.message;
		window.alert(errorCode +": "+ errorMessage);
	});
}

function Logout(){	
	authRef.signOut().then(function() {	  	
		// Sign-out successful.
		window.location.href = "login.html";
		window.alert("Sign-out successful!")
		}).catch(function(error) {
		// An error happened.
		var errorCode = error.code;
		var errorMessage = error.message;
		window.alert(errorCode +": "+ errorMessage);
	});
}

function GetCurrentUser(){
	authRef.onAuthStateChanged(function(user) {
		if (user) {
			// User is signed in.
			document.getElementById("user_name").innerHTML = user.email;
			} else {
			// No user is signed in.
			window.location.href = "login.html";
		}
	});
}

function AddNewMember(){
	const membername = document.querySelector("#member_name").value;
	const memberpass = document.querySelector("#member_password").value;
	const currUser = authRef.currentUser;
	docRef.doc("mods/"+currUser.uid+"/members/"+membername).set({
		//name: membername,
		password: memberpass
		}).then(function(){
		window.alert("Add member: "+membername+" successful!");
		}).catch(function(error) {
		// An error happened.
		var errorCode = error.code;
		var errorMessage = error.message;
		window.alert(errorCode +": "+ errorMessage);
	});
}