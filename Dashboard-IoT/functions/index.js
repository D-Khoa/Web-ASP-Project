// // Create and Deploy Your First Cloud Functions
// // https://firebase.google.com/docs/functions/write-firebase-functions
//
// exports.helloWorld = functions.https.onRequest((request, response) => {
//  response.send("Hello from Firebase!");
// });
// The Cloud Functions for Firebase SDK to create Cloud Functions and setup triggers.
const functions = require('firebase-functions');

// The Firebase Admin SDK to access the Firebase Realtime Database.
const admin = require('firebase-admin');
admin.initializeApp();

exports.checkIoTstatus = functions
    .database
    .ref('/mods/{userID}/status')
    .onWrite((change, context) => {
        const userID = context.params.userID;
        var stt = change.after.val();
        //console.log('status:', userID, stt);
        const actRef = admin.database().ref(`mods/${userID}/actutors/`);
        actRef.once('value').then(function (snap) {
            snap.forEach(function (childsnap) {
                //console.log(childsnap.key);
                sleep(3000, userID, childsnap.key);
            });
        });
        stt = false;
        //console.log('status:', userID, stt);
        return change.after.ref.parent.child('status').set(stt);
    });

function sleep(milliseconds, userID, actId) {
    const date = new Date;
    date.setHours(date.getHours() + 7);
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');
    const actRef = admin.database().ref(`mods/${userID}/actutors/${actId}`);
    console.log(hours + ":" + minutes);
    actRef.once('value', snap => {
        if (snap.exists()) {
            //console.log(snap.key);
            if (snap.hasChild('timerON')) {
                var t = snap.child('timerON').val().split(':');
                if (hours == t[0] && minutes == t[1] && !snap.child('state').val()) {
                    actRef.child('state').set(true);
                    console.log(actId + " ON");
                }
            }
            if (snap.hasChild('timerOFF')) {
                var t = snap.child('timerOFF').val().split(':');
                if (hours == t[0] && minutes == t[1] && snap.child('state').val()) {
                    actRef.child('state').set(false);
                    console.log(actId + " OFF");
                }
            }
        }
    }).catch(function (error) {
        console.log(error);
    });
    let currentDate = null;
    do {
        currentDate = new Date;
        currentDate.setHours(currentDate.getHours() + 7);
    } while (currentDate - date < milliseconds);
}