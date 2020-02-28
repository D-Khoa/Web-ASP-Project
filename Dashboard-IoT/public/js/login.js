var isModLogin = true;
//Register mod user
function Register(){
	const usermail = document.querySelector("#user_mail").value;
	const userpass = document.querySelector("#password").value;
	authRef.createUserWithEmailAndPassword(usermail, userpass).then(function(){
		const currUser = authRef.currentUser;
		currUser.sendEmailVerification().then(function() {
			docRef.doc("mods/"+currUser.uid).set({
				email: currUser.email,
				btnactutors:"",
				btnsensors:"",
			});
			dbRef.ref("mods/"+currUser.uid).set({
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
//Login mod user
function Login(){
	const usermail = document.querySelector("#user_mail").value;
	const userpass = document.querySelector("#password").value;
	authRef.signInWithEmailAndPassword(usermail, userpass).then(function(){
		const currUser = authRef.currentUser;
		if(currUser.emailVerified){
			window.alert("Welcome " + usermail);
			//document.getElementById("login_form").submit();
			var queryString = "?para1=" + true + "&para2=" + usermail;
			window.location.href = "index.html"+queryString;
		}
		else window.alert("Please verify your account before log-in");
		}).catch(function(error) {
		// Handle Errors here.
		var errorCode = error.code;
		var errorMessage = error.message;
		window.alert(errorCode +": "+ errorMessage);
	});
}
//Add new member
function AddNewMember(){
	const membername = document.querySelector("#member_name").value;
	const memberpass = document.querySelector("#member_password").value;
	const currUser = authRef.currentUser;
	docRef.doc("users/"+membername).set({
		uid: currUser.uid,
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
//Member login
function MemberLogin(){
	const inputname = document.querySelector("#member_name").value;
	const inputpass = document.querySelector("#member_password").value;
	docRef.doc("users/"+inputname).get().then(function(doc){
		if(doc.exists){
			if(inputpass === doc.data().password){
				window.alert("Welcome "+inputname);
				var queryString = "?para1=" + true + "&para2=" + usermail;
				window.location.href = "index.html"+queryString;
			}
			else window.alert("Wrong password!");				
		}
		else window.alert("User is not exists!");
	})
}

function ChangeLogin(){
	if(isModLogin){
		isModLogin = false;
		document.getElementById("mem_login_form").style.display = 'block';
		document.getElementById("login_form").style.display = 'none';		
	}
	else{
		isModLogin = true;
		document.getElementById("mem_login_form").style.display = 'none';
		document.getElementById("login_form").style.display = 'block';		
	}	
}