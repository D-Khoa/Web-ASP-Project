function AutoAddItems() {
  var arrayActID, arraySenID;
  authRef.onAuthStateChanged(function(user) {
    if (user != null) {
      const userRef = dbRef.ref("mods/" + user.uid);
      userRef
        .once("value")
        .then(function(snapshot) {
          if (snapshot.exists()) {
            arrayActID = snapshot.val().btnactutors.split(",");
            arraySenID = snapshot.val().btnsensors.split(",");
            arrayActID.forEach(element => AddActutorBtn(element));
            arraySenID.forEach(element => AddSensorLabel(element));
          }
        })
        .catch(function(error) {
          console.log("Got an error: ", error);
        });
    } else {
      // No user is signed in.
      window.location.href = "login.html";
    }
  });
}

function AddActutorBtn(actId) {
  if (actId === "") return;
  if (document.getElementById(actId)) {
    window.alert("Actutor:" + actId + " is exists");
    return;
  }
  const currUser = authRef.currentUser;
  const userRef = dbRef.ref("mods/" + currUser.uid + "/actutors/" + actId);
  const actField = document.querySelector("#actutor_bar");
  var element = document.createElement("button");

  element.id = actId;
  actField.appendChild(element);
  UpdateUserActutor(actId);
  userRef
    .once("value")
    .then(function(snapshot) {
      if (snapshot.exists()) {
        element.innerHTML = snapshot.val().name + "-" + actId;
        if (snapshot.val().state) {
          element.className = "btn btn-large btn-success btn-actutor";
        } else {
          element.className = "btn btn-large btn-inverse btn-actutor";
        }
      } else {
        window.alert("This actutor is not exists!");
        element.parentNode.removeChild(element);
      }
    })
    .catch(function(error) {
      console.log("Got an error: ", error);
    });

  element.onclick = function() {
    userRef
      .once("value")
      .then(function(snapshot) {
        if (snapshot.exists()) {
          element.innerHTML = snapshot.val().name + "-" + actId;
          if (!snapshot.val().state) {
            element.className = "btn btn-large btn-success btn-actutor";
          } else {
            element.className = "btn btn-large btn-inverse btn-actutor";
          }
          userRef
            .update({
              state: !snapshot.val().state
            })
            .then(function() {
              console.log("Update successful!");
            })
            .catch(function(error) {
              console.log("Got an error: ", error);
            });
        }
      })
      .catch(function(error) {
        console.log("Got an error: ", error);
      });
  };
}

var sensordata = 0;
var data = [],
  totalPoints = 300;
function AddSensorLabel(sensorID) {
  if (sensorID === "") return;
  if (document.getElementById(sensorID)) {
    window.alert("Sensor:" + sensorID + " is exists");
    return;
  }
  const currUser = authRef.currentUser;
  const userRef = dbRef.ref("mods/" + currUser.uid + "/sensors/" + sensorID);
  const sensorField = document.querySelector("#sensor_bar");
  var element = document.createElement("span");
  var isExist = true;
  element.id = sensorID;
  UpdateUserSensor(sensorID);
  userRef.once("value").then(function(snapshot) {
    if (snapshot.exists()) {
      element.innerHTML = snapshot.val().name + ": " + snapshot.val().value;
      element.style.cursor = "pointer";
      if (snapshot.val().state) {
        element.className = "label label-success label-sensor";
      } else {
        element.className = "label label-sensor";
      }
    } else {
      window.alert("This sensor is not exists!");
      isExist = false;
    }
  });
  element.onclick = function() {
    const chartbox = document.querySelector("#chartbox");
    var chart = document.querySelector("#realtimechart");
    chart.parentNode.removeChild(chart);
    chart = document.createElement("div");
    chart.id = "realtimechart";
    chart.style = "height:190px";
    chartbox.appendChild(chart);
    data = Array(totalPoints).fill(0);
    sensordata = 0;
    charts();
    userRef.on("value", function(snapshot) {
      sensordata = snapshot.val().value;
      document.querySelector("#realtimebox").innerHTML =
        sensorID + " - " + snapshot.val().name;
      element.innerHTML = snapshot.val().name + ": " + snapshot.val().value;
      if (snapshot.val().state) {
        element.className = "label label-success label-sensor";
      } else {
        element.className = "label label-sensor";
      }
    });
  };
  if (!isExist) userRef.off("value", onValueChange);
  if (isExist) sensorField.appendChild(element);
  else element.parentNode.removeChild(element);
}

function UpdateUserActutor(itemID) {
  const currUser = authRef.currentUser;
  const userRef = dbRef.ref("mods/" + currUser.uid);
  var itemArray = null;
  userRef
    .once("value")
    .then(function(snapshot) {
      if (snapshot.exists()) {
        itemArray = snapshot.val().btnactutors;
        if (!itemArray.includes(itemID)) {
          if (itemArray != null && itemArray != "") itemArray += "," + itemID;
          else itemArray = itemID;
        }
        userRef
          .update({
            btnactutors: itemArray
          })
          .then(function() {
            console.log("Update successful!");
          })
          .catch(function(error) {
            console.log("Got an error: ", error);
          });
      }
    })
    .catch(function(error) {
      console.log("Got an error: ", error);
    });
}

function UpdateUserSensor(itemID) {
  const currUser = authRef.currentUser;
  const userRef = dbRef.ref("mods/" + currUser.uid);
  var itemArray = null;
  userRef
    .once("value")
    .then(function(snapshot) {
      if (snapshot.exists()) {
        itemArray = snapshot.val().btnsensors;
        if (!itemArray.includes(itemID)) {
          if (itemArray != null && itemArray != "") itemArray += "," + itemID;
          else itemArray = itemID;
        }
        userRef
          .update({
            btnsensors: itemArray
          })
          .then(function() {
            console.log("Update successful!");
          })
          .catch(function(error) {
            console.log("Got an error: ", error);
          });
      }
    })
    .catch(function(error) {
      console.log("Got an error: ", error);
    });
}

function AddNewActutor() {
  const actId = document.querySelector("#actutor_id").value;
  const actname = document.querySelector("#actutor_name").value;
  const currUser = authRef.currentUser;
  const userRef = dbRef.ref("mods/" + currUser.uid + "/actutors/" + actId);
  userRef
    .set({
      name: actname,
      state: false
    })
    .then(function() {
      window.alert("Add actutor: " + actId + " successful!");
    })
    .catch(function(error) {
      console.log("Got an error: ", error);
    });
}

function AddNewSensor() {
  const senID = document.querySelector("#sensor_id").value;
  const senName = document.querySelector("#sensor_name").value;
  const currUser = authRef.currentUser;
  const userRef = dbRef.ref("mods/" + currUser.uid + "/sensors/" + senID);
  userRef
    .set({
      name: senName,
      state: false,
      value: 0
    })
    .then(function() {
      window.alert("Add sensor: " + senID + " successful!");
    })
    .catch(function(error) {
      console.log("Got an error: ", error);
    });
}
