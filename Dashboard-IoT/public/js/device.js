function AutoAddItems(){
	var docRef = firebase.firestore();
	var user = firebase.auth();
	var arrayActID,arraySenID;
	user.onAuthStateChanged(function(user){
		if(user!= null){
			docRef.doc("users/" + user.uid).get().then(function(doc){
				if(doc.exists){
					arrayActID = doc.data().actutors.split(',');
					arraySenID = doc.data().sensors.split(',');
					arrayActID.forEach(element => AddActutorBtn(element));
					arraySenID.forEach(element => AddSensorLabel(element));
				}
				}).catch(function(error){
				console.log("Got an error: ", error);
			});
		}
	});
}

function AddActutorBtn(actId){
	if(actId === "") return;
	var docRef = firebase.firestore();
	var user = firebase.auth().currentUser;
	var element = document.createElement('button');
	//var actId = document.querySelector('#actutor_id').value;
	var actField = document.querySelector('#actutor_bar');
	
	element.id = actId;	
	actField.appendChild(element);
	docRef.doc("actutors/" + actId).get().then(function(doc){
		if(doc.exists){
			UpdateUserActutor(actId, user.uid);
			element.innerHTML = doc.data().name + "-" + actId;
			if(doc.data().state){
				element.className = "btn btn-large btn-success"
			}
			else{
				element.className ="btn btn-large btn-inverse";
			}
		}
		else{
			console.log("This actutor is not exists!");
			element.parentNode.removeChild(element);
		}
	}).catch(function(error){console.log("Got an error: ", error)});
	
	element.onclick = function(){
		docRef.doc("actutors/" + actId).get().then(function(doc){
			if(doc.exists){
				element.innerHTML = doc.data().name + "-" + actId;
				if(!doc.data().state){
					element.className = "btn btn-large btn-success";					
				}
				else{
					element.className ="btn btn-large btn-inverse";
				}
				docRef.doc("actutors/" + actId).update({	
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
	var docRef = firebase.firestore();
	var user = firebase.auth().currentUser;
	var element = document.createElement('span');
	//var sensorID = document.querySelector('#sensor_id').value;
	var sensorField = document.querySelector('#sensor_bar');
	var isExist = true;
	element.id = sensorID;	
	docRef.doc("sensors/" + sensorID).onSnapshot(function(doc){
		if(doc && doc.exists){
			UpdateUserSensor(sensorID,user.uid);
			element.innerHTML = doc.data().name + ": " + doc.data().value;
			if(doc.data().state){
				element.className = "label label-success";
			}
			else{
				element.className ="label";
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

function UpdateUserActutor(itemID, userid){
	var docRef = firebase.firestore();
	var itemArray = null;
	docRef.doc("users/" + userid).get().then(function(doc){
		if(doc.exists){
			itemArray = doc.data().actutors;
			if(!itemArray.includes(itemID)){
				if(itemArray != null)	itemArray += ',' + itemID;
				else itemArray = itemID;
			}
			docRef.doc("users/" + userid).update({
				actutors: itemArray
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

function UpdateUserSensor(itemID, userid){
	var docRef = firebase.firestore();
	var itemArray = null;
	docRef.doc("users/" + userid).get().then(function(doc){
		if(doc.exists){
			itemArray = doc.data().sensors;
			if(!itemArray.includes(itemID))
			{
				if(itemArray != null && itemArray != "")	itemArray += ',' + itemID;
				else itemArray = itemID;
			}
			docRef.doc("users/" + userid).update({
				sensors: itemArray
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
	var docRef = firebase.firestore();
	var actId = document.querySelector('#actutor_id').value;
	var actname = document.querySelector('#actutor_name').value;
	docRef.doc("actutors/" + actId).set({
		name : actname,
		state : false
		}).then(function(){
		console.log("Register successful!");
		}).catch(function(error){
		console.log("Got an error: ", error);
	});
}

function AddNewSensor(){
	var docRef = firebase.firestore();
	var senID = document.querySelector('#sensor_id').value;
	var senName = document.querySelector('#sensor_name').value;
	docRef.doc("sensors/" + senID).set({
		name : senName,
		state : false,
		value : 0
		}).then(function(){
		console.log("Register successful!");
		}).catch(function(error){
		console.log("Got an error: ", error);
	});
}