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
    .region('asia-northeast1')
    .database
    .ref('/mods/{userID}/status')
    .onWrite((change, context) => {
        const userID = context.params.userID;
        var stt = change.after.val();
        console.log('status:', userID, stt);
        sleep(3000);
        stt = false;
        console.log('status:', userID, stt);
        return change.after.ref.parent.child('status').set(stt);
    });

function sleep(milliseconds) {
    const date = Date.now();
    let currentDate = null;
    do {
        currentDate = Date.now();
    } while (currentDate - date < milliseconds);
}