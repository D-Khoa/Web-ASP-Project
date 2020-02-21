function actutorItem(id, name, state){
	this.id = id;
	this.name = name;
	this.state = state;
}

function AddElementHtml(type) {
	const docRef = firebase.firestore();
	var itemname = document.querySelector("#item_name");
	var sensorbar = document.querySelector("#sensor_bar");
	var actutorbar = document.querySelector("#actutor_bar");
	var elabel = document.createElement("label");
	var element = document.createElement("input");
	var actname, actstate;	
	element.type = type;
	element.id = itemname.value;

	if(type === 'button'){		
		element.name = itemname.value;
		element.value = itemname.value;
		if(actstate) {
			element.style.background = "green";
		}
		else {
			element.style.background = "yellow";
		}
		
		element.addEventListener("click", function(){
			docRef.doc("actutors/" + itemname.value).get().then(function(doc){
				if(doc && doc.exists){
					actname = doc.data().name;
					actstate = doc.data().state;
				}
			}).catch(function(error){
				console.log("Got an error: ", error);
			});
			if(actstate) {
				element.style.background = "yellow";				
			}
			else {
				element.style.background = "green";
			}
			SetActutor(itemname.value, actname, !actstate);
		});
		actutorbar.appendChild(element);
	}
	else { 			
		elabel.htmlFor = element.id;
		if(element.id.includes("Temp")){
			elabel.innerHTML = "Nhiet Do";
		}
		sensorbar.appendChild(elabel);
		sensorbar.appendChild(element);
		GetSensor(itemname.value,element.id);
	}
}

function SetActutor(id, name, value){
	const docRef = firebase.firestore();	
	docRef.doc("actutors/" + id+"/").set({
		name : name,
		state : value
	}).then(function(){
		console.log("Update this actutor");
	}).catch(function(error){
		console.log("Got an error: ", error);
	});			
}

function GetAcutor(id){
	const docRef = firebase.firestore();
	return docRef.doc("actutors/" + id).get().then(function(doc){
		if(doc && doc.exists){

		}
	}).catch(function(error){
		console.log("Got an error: ", error);
	});
}

function SetSensor(id, name, state, value){
	const docRef = firebase.firestore();
	docRef.doc("sensors/" + id).set({
		name: name,
		state : state,
		value : value
	}).then(function(){
		console.log("Register successful!");
	}).catch(function(error){
		console.log("Got an error: ", error);
	});
}

function GetSensor(id, elementid){
	const docRef = firebase.firestore();
	docRef.doc("sensors/" + id).onSnapshot(function(doc){
		if(doc && doc.exists){
			const mydata = doc.data();
			const textfield = document.querySelector("#"+elementid);
			textfield.value = mydata.value;
		}
	})
}