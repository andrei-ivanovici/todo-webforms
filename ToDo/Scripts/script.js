
console.log("Loaded!");

$("#task").on('keydown', (ev) => {
    if (ev.keyCode == 13) {
        console.log("hello");
        $("#newTodo").submit();
    }
})