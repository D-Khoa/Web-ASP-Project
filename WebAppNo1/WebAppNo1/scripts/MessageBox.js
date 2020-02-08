var button = document.getElementById('clickme');
setTimeout(function () { document.write("Hi!"); }, 3000);
button.addEventListener('click', function () { alert("Hello World!"); });
