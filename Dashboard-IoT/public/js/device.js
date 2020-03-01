function AutoAddItems(){
	var arrayActID,arraySenID;
	authRef.onAuthStateChanged(function(user){
		if(user!= null){
			const userRef = docRef.doc("mods/"+user.uid);
			userRef.get().then(function(doc){
				if(doc.exists){
					arrayActID = doc.data().btnactutors.split(',');
					arraySenID = doc.data().btnsensors.split(',');
					arrayActID.forEach(element => AddActutorBtn(element));
					arraySenID.forEach(element => AddSensorLabel(element));
				}
				}).catch(function(error){
				console.log("Got an error: ", error);
			});
		}
		else {
			// No user is signed in.
			window.location.href = "login.html";			
		}
	});
}

function AddActutorBtn(actId){
	if(actId === "") return;
	if(document.getElementById(actId))return;
	const currUser = authRef.currentUser;
	const userRef = docRef.doc("mods/"+ currUser.uid + "/actutors/" + actId);
	const actField = document.querySelector('#actutor_bar');
	var element = document.createElement('button');
	
	element.id = actId;	
	actField.appendChild(element);
	userRef.get().then(function(doc){
		if(doc.exists){
			UpdateUserActutor(actId);
			element.innerHTML = doc.data().name + "-" + actId;
			if(doc.data().state){
				element.className = "btn btn-large btn-success btn-actutor";
			}
			else{
				element.className ="btn btn-large btn-inverse btn-actutor";
			}
		}
		else{
			console.log("This actutor is not exists!");
			element.parentNode.removeChild(element);
		}
	}).catch(function(error){console.log("Got an error: ", error)});
	
	element.onclick = function(){
		userRef.get().then(function(doc){
			if(doc.exists){
				element.innerHTML = doc.data().name + "-" + actId;
				if(!doc.data().state){
					element.className = "btn btn-large btn-success btn-actutor";					
				}
				else{
					element.className ="btn btn-large btn-inverse btn-actutor";
				}
				userRef.update({	
					state : !doc.data().state
					}).then(function(){
					console.log("Update successful!");
					}).catch(function(error){
					console.log("Got an error: ", error);
				});
			}}).catch(function(error){console.log("Got an error: ", error)});
	}	
}

function AddSensorLabel(sensorID){
	const currUser = authRef.currentUser;
	const userRef = docRef.doc("mods/" + currUser.uid + "/sensors/" + sensorID);
	const sensorField = document.querySelector('#sensor_bar');
	var element = document.createElement('span');
	var isExist = true;
	element.id = sensorID;	
	userRef.onSnapshot(function(doc){
		if(doc && doc.exists){
			UpdateUserSensor(sensorID);
			element.innerHTML = doc.data().name + ": " + doc.data().value;
			if(doc.data().state){
				element.className = "label label-success label-sensor";
			}
			else{
				element.className ="label label-sensor";
			}
		}
		else{
			console.log("This sensor is not exists!");
			isExist = false;
		}
	});
	if(isExist)	sensorField.appendChild(element);
	else element.parentNode.removeChild(element);
}

function UpdateUserActutor(itemID){
	const currUser = authRef.currentUser;
	const userRef = docRef.doc("mods/"+currUser.uid);
	var itemArray = null;
	userRef.get().then(function(doc){
		if(doc.exists){
			itemArray = doc.data().btnactutors;
			if(!itemArray.includes(itemID)){
				if(itemArray != null && itemArray != "")	itemArray += ',' + itemID;
				else itemArray = itemID;
			}
			userRef.update({
				btnactutors: itemArray
				}).then(function(){
				console.log("Update successful!");
				}).catch(function(error){
				console.log("Got an error: ", error);
			});
		}
		}).catch(function(error){
		console.log("Got an error: ", error);
	});	
}	

function UpdateUserSensor(itemID){
	const currUser = authRef.currentUser;
	const userRef = docRef.doc("mods/"+currUser.uid);
	var itemArray = null;
	userRef.get().then(function(doc){
		if(doc.exists){
			itemArray = doc.data().btnsensors;
			if(!itemArray.includes(itemID))
			{
				if(itemArray != null && itemArray != "")	itemArray += ',' + itemID;
				else itemArray = itemID;
			}
			userRef.update({
				btnsensors: itemArray
				}).then(function(){
				console.log("Update successful!");
				}).catch(function(error){
				console.log("Got an error: ", error);
			});
		}
		}).catch(function(error){
		console.log("Got an error: ", error);
	});
}

function AddNewActutor(){
	const actId = document.querySelector('#actutor_id').value;
	const actname = document.querySelector('#actutor_name').value;
	const currUser = authRef.currentUser;
	const userRef = docRef.doc("mods/"+currUser.uid+"/actutors/" + actId);
	userRef.set({
		name : actname,
		state : false
		}).then(function(){
		console.log("Register successful!");
		}).catch(function(error){
		console.log("Got an error: ", error);
	});
}

function AddNewSensor(){
	const senID = document.querySelector('#sensor_id').value;
	const senName = document.querySelector('#sensor_name').value;
	const currUser = authRef.currentUser;
	const userRef = docRef.doc("mods/"+currUser.uid+"/sensors/" + senID);
	userRef.set({
		name : senName,
		state : false,
		value : 0
		}).then(function(){
		console.log("Register successful!");
		}).catch(function(error){
		console.log("Got an error: ", error);
	});
}			