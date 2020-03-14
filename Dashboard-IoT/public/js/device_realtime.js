function CheckIoTStatus() {
  const iotboard = document.getElementById("iot_board");
  const iotstt = document.getElementById("iot_stt");
  const userRef = dbRef.ref("mods/" + userID);
  userRef.on('value', function (snapshot) {
    var stt = snapshot.val().status
    if (stt) {
      iotboard.style.backgroundColor = "#009f75";
      iotboard.style.color = "#fff";
      iotboard.style.textShadow = "none";
      iotboard.style.padding = "10px";
      iotstt.style.textShadow = "none";
      iotstt.innerText = "IoT Board - Online";
    }
    else {
      iotboard.style.backgroundColor = "#ef4444";
      iotboard.style.color = "#000";
      iotboard.style.textShadow = "none";
      iotboard.style.padding = "10px";
      iotstt.style.textShadow = "none";
      iotstt.innerText = "IoT Board - Offline";
    }
  });
}

//Add new actutor for current user
function AddNewActutor() {
  //Get id and name of actutor
  const actId = document.querySelector("#newactutor_id").value;
  const actname = document.querySelector("#newactutor_name").value;
  //Address of actutor
  const userRef = dbRef.ref("mods/" + userID + "/actutors/" + actId);
  //Create actutor
  userRef
    .set({
      name: actname,
      state: false
    })
    .then(function () {
      window.alert("Add actutor: " + actId + " successful!");
    })
    .catch(function (error) {
      console.log("Got an error: ", error);
    });
}

//Add new sensor for current user
function AddNewSensor() {
  //Get id and name of sensor
  const senID = document.querySelector("#newsensor_id").value;
  const senName = document.querySelector("#newsensor_name").value;
  const senUnit = document.querySelector("#newsensor_unit").value;
  //Address of sensor
  const userRef = dbRef.ref("mods/" + userID + "/sensors/" + senID);
  //Create sensor
  userRef
    .set({
      name: senName,
      state: false,
      unit: senUnit,
      value: 0
    })
    .then(function () {
      window.alert("Add sensor: " + senID + " successful!");
    })
    .catch(function (error) {
      console.log("Got an error: ", error);
    });
}

//Add actutors buttons into index.html page with actutor id
function AddActutorBtn(actId) {
  if (actId == null) {
    actId = prompt("Input actutor ID:");
  }
  //If actutor empty then skip
  if (actId === null || actId === "") return;
  //If actutor buttons is exists in index.html page then alert
  if (document.getElementById(actId)) {
    window.alert("Actutor:" + actId + " is exists");
    return;
  }
  //Address for actutor
  const userRef = dbRef.ref("mods/" + userID + "/actutors/" + actId);
  //Parent of actutor buttons
  const actField = document.querySelector("#actutor_bar");
  const timerField = document.querySelector("#timer_bar");
  //Create new button
  var element = document.createElement("button");
  var timereOnlement = document.createElement("div");
  var timereOfflement = document.createElement("div");
  //Button id = actutor id
  element.id = actId;
  timereOnlement.id = actId + "_timerON";
  timereOfflement.id = actId + "_timerOFF";
  //Add actutor button into parent
  actField.appendChild(element);
  userRef
    .once("value")
    .then(function (snapshot) {
      if (snapshot.exists()) {
        //Text on actutor button
        element.innerHTML = snapshot.val().name + "-" + actId;
        //If actutor change state then change color button
        if (snapshot.val().state)
          element.className = "btn btn-large btn-success btn-actutor";
        else element.className = "btn btn-large btn-inverse btn-actutor";
        //Update list button actutors
        UpdateUserActutor(actId);
        if (snapshot.hasChild("timerON")) {
          timereOnlement.style.padding = "5px";
          timereOnlement.style.background = "#009f75";
          timereOnlement.innerHTML = actId + "_timerON: " + snapshot.child("timerON").val();
          timerField.appendChild(timereOnlement);
        }
        if (snapshot.hasChild("timerOFF")) {
          timereOfflement.style.padding = "5px";
          timereOfflement.style.background = "#ef4444";
          timereOfflement.innerHTML = actId + "_timerOFF: " + snapshot.child("timerOFF").val();
          timerField.appendChild(timereOfflement);
        }
      } else {
        window.alert("This actutor is not exists!");
        element.parentNode.removeChild(element);
      }
    })
    .catch(function (error) {
      console.log("Got an error: ", error);
    });
  //Click event
  element.addEventListener("click", function () {
    userRef
      .once("value")
      .then(function (snapshot) {
        if (snapshot.exists()) {
          //Get actutor state
          if (!snapshot.val().state)
            element.className = "btn btn-large btn-success btn-actutor";
          else element.className = "btn btn-large btn-inverse btn-actutor";
          //Update actutor state
          userRef
            .update({
              state: !snapshot.val().state
            })
            .then(function () {
              console.log("Update successful!");
            })
            .catch(function (error) {
              console.log("Got an error: ", error);
            });
        }
      })
      .catch(function (error) {
        console.log("Got an error: ", error);
      });
  });
}

//Remove actutor button
function RemoveActutorButton(actId) {
  if (actId == null) {
    actId = prompt("Input actutor ID:");
  }
  if (actId === null || actId === "") return;
  const actbtn = document.getElementById(actId);
  if (actbtn != null) actbtn.parentNode.removeChild(actbtn);
  userRef.once("value").then(function (snapshot) {
    if (snapshot.exists()) {
      itemArray = snapshot.val().btnactutors;
      if (itemArray.includes(actId)) {
        itemArray = itemArray.replace(',' + actId, '');
        userRef.update({
          btnactutors: itemArray
        }).then(function () {
          console.log("Removed: ", actId);
        }).catch(function (error) {
          console.log("Got an error: ", error);
        });
      }
    }
  }).catch(function (error) {
    console.log("Got an error: ", error);
  });
}

//Set sensor data = 0
var sensordata = 0;
//Set chart array data = 0
var data = [],
  totalPoints = 300;
var tempID = "None";
//Add label sensor with sensor id
function AddSensorLabel(sensorID) {
  if (sensorID == null) {
    sensorID = prompt("Input sensor ID:");
  }
  //If sensor id empty then skip
  if (sensorID === null | sensorID === "") return;
  //If label sensor is exist then alert
  if (document.getElementById(sensorID)) {
    window.alert("Sensor:" + sensorID + " is exists");
    return;
  }
  //Address of sensor
  const userRef = dbRef.ref("mods/" + userID + "/sensors/" + sensorID);
  //Parent of label sensor
  const sensorField = document.querySelector("#sensor_bar");
  //Create new span contains label sensor
  var element = document.createElement("span");
  //Set label id = sensor id
  element.id = sensorID;
  element.style.cursor = "pointer";
  //Add label sensor into parent
  sensorField.appendChild(element);
  //Get info of sensor
  userRef.on("value", function (snapshot) {
    if (snapshot.exists()) {
      element.name = snapshot.val().name;
      element.innerHTML = snapshot.val().name + ": " + snapshot.val().value;
      if (snapshot.val().state)
        element.className = "label label-success label-sensor";
      else element.className = "label label-sensor";
      //Update list sensor of user
      UpdateUserSensor(sensorID);
    } else {
      window.alert("This sensor is not exists!");
      element.parentNode.removeChild(element);
      return;
    }
  });
  var btnClick = false;
  //Click event
  element.addEventListener("click", function () {
    btnClick = true;
    if (btnClick) tempID = element.id;
    //Get chartbox and chart from index.html
    const chartbox = document.querySelector("#chartbox");
    var chart = document.querySelector("#realtimechart");
    //Remove old chart and create a new when click
    chart.parentNode.removeChild(chart);
    chart = document.createElement("div");
    chart.id = "realtimechart";
    chart.style = "height:190px";
    chartbox.appendChild(chart);
    //Reset data
    data = Array(totalPoints).fill(0);
    sensordata = 0;
    //Draw new chart
    charts();
  });
}

function AddSensorGauge(sensorID, sensorColor) {
  if (sensorID == null) {
    sensorID = prompt("Input sensor ID:");
  }
  if (sensorColor == null) {
    sensorColor = prompt("Choose color:", "red");
    sensorColor = sensorColor.toLowerCase();
  }
  //If sensor id empty then skip
  if (sensorID === null | sensorID === "") return;
  //Address of sensor
  const userRef = dbRef.ref("mods/" + userID + "/sensors/" + sensorID);
  var element;
  userRef.on("value", function (snapshot) {
    if (snapshot.exists()) {
      if (document.getElementById(sensorID + "_gauge"))
        ChangeGaugeValue(sensorID, snapshot.val().value);
      else
        element = AddGauge("#sensor_bar", sensorID, sensorColor, snapshot.val().unit, snapshot.val().value);

    }
    else {
      alert("This sensor is not exist!");
      userRef.off("value");
    }
  })
  circle_progess();
  var btnClick = false;
  //Click event
  element.addEventListener("click", function () {
    btnClick = true;
    if (btnClick) tempID = element.name;
    //Get chartbox and chart from index.html
    const chartbox = document.querySelector("#chartbox");
    var chart = document.querySelector("#realtimechart");
    //Remove old chart and create a new when click
    chart.parentNode.removeChild(chart);
    chart = document.createElement("div");
    chart.id = "realtimechart";
    chart.style = "height:190px";
    chartbox.appendChild(chart);
    //Reset data
    data = Array(totalPoints).fill(0);
    sensordata = 0;
    //Draw new chart
    charts();
  });
}

function AddGauge(parentID, sensorID, gaugeColor, gaugeUnit, gaugeValue) {
  //Parent of label sensor
  const sensorField = document.querySelector(parentID);
  var element = document.createElement("div");
  element.className = "span2";
  element.addEventListener("onTablet", function () {
    element.className = "span4";
  });
  element.id = sensorID + "_gauge";
  element.name = sensorID;
  var elementbox = document.createElement("div");
  elementbox.id = sensorID + "_gauge_box";
  elementbox.classList.add('circleStatsItemBox');
  elementbox.classList.add(gaugeColor);
  var elementheader = document.createElement("div");
  elementheader.id = sensorID + "_gauge_header";
  elementheader.classList.add("header");
  elementheader.innerHTML = sensorID;
  var elementunit = document.createElement("span");
  elementunit.id = sensorID + "_gauge_unit";
  elementunit.classList.add("percent");
  elementunit.innerHTML = gaugeUnit;
  var elementcirclestat = document.createElement("div");
  elementcirclestat.id = sensorID + "_gauge_stat";
  elementcirclestat.classList.add("circleStat");
  var elementvalue = document.createElement("input");
  elementvalue.id = sensorID + "_gauge_value";
  elementvalue.classList.add("whiteCircle");
  elementvalue.type = "text";
  if (gaugeValue != null) elementvalue.value = gaugeValue;
  else elementvalue.value = "0";

  element.appendChild(elementbox);
  elementbox.appendChild(elementheader);
  elementbox.appendChild(elementunit);
  elementbox.appendChild(elementcirclestat);
  elementcirclestat.appendChild(elementvalue);
  sensorField.appendChild(element);
  return element;
}

function ChangeGaugeValue(sensorID, gaugeValue) {
  const elementvalue = document.getElementById(sensorID + "_gauge_value");
  if (gaugeValue != null) elementvalue.value = gaugeValue;
  else elementvalue.value = "0";
}

//Remove sensor label
function RemoveSensorLabel(sensorID) {
  if (sensorID == null) {
    sensorID = prompt("Input sensor ID:");
  }
  //If sensor id empty then skip
  if (sensorID === null | sensorID === "") return;
  const userRef = dbRef.ref("mods/" + userID);
  const sensorlb = document.getElementById(sensorID);
  var itemArray = null;
  if (sensorlb != null) sensorlb.parentNode.removeChild(sensorlb);
  userRef.once("value").then(function (snapshot) {
    if (snapshot.exists()) {
      itemArray = snapshot.val().btnsensors;
      if (itemArray.includes(sensorID)) {
        itemArray = itemArray.replace(',' + sensorID, '');
        userRef.update({
          btnsensors: itemArray
        }).then(function () {
          console.log("Removed: ", sensorID);
        }).catch(function (error) {
          console.log("Got an error: ", error);
        });
      }
    }
  }).catch(function (error) {
    console.log("Got an error: ", error);
  });
}

//Update actutor for mod user
function UpdateUserActutor(itemID) {
  const userRef = dbRef.ref("mods/" + userID);
  var itemArray = null;
  //Read list actutors of user
  userRef
    .once("value")
    .then(function (snapshot) {
      if (snapshot.exists()) {
        itemArray = snapshot.val().btnactutors;
        //If list actutors isn't contains actutor id then add it
        if (!itemArray.includes(itemID)) {
          if (itemArray != null && itemArray != "") itemArray += "," + itemID;
          else itemArray = itemID;
        }
        //Update list new actutors
        userRef
          .update({
            btnactutors: itemArray
          })
          .then(function () {
            console.log("Update successful!");
          })
          .catch(function (error) {
            console.log("Got an error: ", error);
          });
      }
    })
    .catch(function (error) {
      console.log("Got an error: ", error);
    });
}

//Update user sensors list
function UpdateUserSensor(itemID) {
  const userRef = dbRef.ref("mods/" + userID);
  var itemArray = null;
  //Read list sensors of user
  userRef
    .once("value")
    .then(function (snapshot) {
      if (snapshot.exists()) {
        itemArray = snapshot.val().btnsensors;
        //If list sensors isn't contains sensor then add it
        if (!itemArray.includes(itemID)) {
          if (itemArray != null && itemArray != "") itemArray += "," + itemID;
          else itemArray = itemID;
        }
        //Update new list sensor
        userRef
          .update({
            btnsensors: itemArray
          })
          .then(function () {
            console.log("Update successful!");
          })
          .catch(function (error) {
            console.log("Got an error: ", error);
          });
      }
    })
    .catch(function (error) {
      console.log("Got an error: ", error);
    });
}

//Auto load list actutors and sensors of user when load page
function AutoAddItems() {
  var arrayActID, arraySenID;
  dbRef
    .ref("mods/" + userID)
    .once("value")
    .then(function (snapshot) {
      if (snapshot.exists()) {
        arrayActID = snapshot.val().btnactutors.split(",");
        arraySenID = snapshot.val().btnsensors.split(",");
        arrayActID.forEach(element => AddActutorBtn(element));
        //arraySenID.forEach(element => AddSensorLabel(element));
        arraySenID.forEach(element => AddSensorGauge(element, "red"));
      } else {
        // No user is signed in.
        window.location.href = "login.html";
      }
    })
    .catch(function (error) {
      console.log("Got an error: ", error);
    });
}

function AddNewTimer(actID, timeSet, switchState) {
  if (actID == null) {
    actID = prompt("Input actutor ID:");
  }
  if (timeSet == null) {
    var timeSeth = 24;
    while (timeSeth > 23 || isNaN(timeSeth)) {
      timeSeth = prompt("Input hours:", "");
    }
    var timeSetm = 60;
    while (timeSetm > 59 || isNaN(timeSetm)) {
      timeSetm = prompt("Input minute:", "");
    }
    timeSet = timeSeth + ":" + timeSetm;
  }
  if (switchState == null) {
    switchState = prompt("Do you want button turn on or turn off? Turn (ON/OFF default:ON): ", "ON")
    switchState = switchState.toUpperCase();
  }
  if (actID == null || actID == "" || timeSet == null || timeSet == "") return;
  const timerID = "timer" + switchState;
  const actutorRef = dbRef.ref("mods/" + userID + "/actutors/" + actID);
  actutorRef.once('value').then(function (snapshot) {
    if (snapshot.exists()) {
      actutorRef.child(timerID).set(timeSet)
        .then(function () {
          console.log(timerID + " : " + timeSet);
        }).catch(function (error) {
          console.log(error);
        });
      alert("Set " + snapshot.key + " " + switchState + " at " + timeSet);
    }
    else {
      alert("This actutor id is not exist! Please check and try again!");
    }
  });
  location.reload();
}

function RemoveTimerButton(actID, switchState) {
  if (actID == null) {
    actID = prompt("Input actutor ID:");
  }
  if (switchState == null) {
    switchState = prompt("Do you want button turn on or turn off? Turn (ON/OFF default:ON): ", "ON")
    switchState = switchState.toUpperCase();
  }
  if (actID == null || actID == "") return;
  const timerID = "timer" + switchState;
  const actutorRef = dbRef.ref("mods/" + userID + "/actutors/" + actID + "/" + timerID);
  actutorRef.once("value").then(function (snapshot) {
    if (snapshot.exists()) {
      if (confirm("Are you sure delete timer " + switchState + " " + actID + " at " + snapshot.val())) {
        actutorRef.remove().then(function () {
          location.reload();
          alert("Timer removed!");
        });
      }
    }
    else {
      alert("Timer is not exist. Please check again!");
    }
  }).catch(function (error) {
    console.log(error);
  });
}