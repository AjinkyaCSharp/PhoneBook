// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
export const firebaseConfig = {
  apiKey: "AIzaSyC7JFs_ZRMu4GfxxDSUYh60yRpcoHKTh1c",
  authDomain: "phonebook-3e7dc.firebaseapp.com",
  projectId: "phonebook-3e7dc",
  storageBucket: "phonebook-3e7dc.appspot.com",
  messagingSenderId: "607551047943",
  appId: "1:607551047943:web:9df6a4c4755dbf8ca19da2",
  measurementId: "G-24HC5S2LS0"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);