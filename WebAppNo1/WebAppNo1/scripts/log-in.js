firebase.auth().onAuthStateChanged(function(user) {
	if (user) {
	    // User is signed in.
	    var user = firebase.auth().currentUser;
if(user != null)
{
	document.getElementById("user_para").innerHTML = "Welcome " + user.email + " has logged in.";
	document.getElementById("login_box").style.display = 'none';
	document.getElementById("top_bar").style.display = 'block';
	document.getElementById("container_bar").style.display = 'block';
}		
} 
else {
		// No user is signed in.
		document.getElementById("user_para").innerHTML = "None";
document.getElementById("login_box").style.display = 'block';
document.getElementById("top_bar").style.display = 'none';
document.getElementById("container_bar").style.display = 'none';
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
	//window.location.href = 'dashboard.html';
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

function writeDeviceData() {
	var deviceCD = document.getElementById("deviceCD").value;
	var deviceState = document.getElementById("deviceState").value;
	var deviceValue = document.getElementById("deviceValue").value;
	var deviceLocation = document.getElementById("deviceLocation").value;

	firebase.database().ref('devices/' + deviceCD).set({
		state : deviceState,
		value : deviceValue,
		location : deviceLocation
	}).catch(function(error){
		var errorMessage = error.message;
		window.alert("Error: " + errorMessage);
	});
	window.alert("Successful: " + deviceCD);
}

function readUserData(){
	var deviceCD = document.getElementById("findeviceCD").value;
	var deviceState = document.getElementById("get_device_state").value;
	var deviceValue = document.getElementById("get_device_value").value;
	var deviceLocation = document.getElementById("get_device_location").value;

	return firebase.database().ref('/devices/' + deviceCD).once('value').then(function(snapshot) {
		deviceState = (snapshot.val() && snapshot.val().state) || 'Empty';
		deviceValue = (snapshot.val() && snapshot.val().value) || 'Empty';
		deviceLocation = (snapshot.val() && snapshot.val().location) || 'Empty';
	  // ...
	  document.getElementById("get_device_state").innerHTML = deviceState;
	  document.getElementById("get_device_value").innerHTML = deviceValue;
	  document.getElementById("get_device_location").innerHTML = deviceLocation;
	});
}