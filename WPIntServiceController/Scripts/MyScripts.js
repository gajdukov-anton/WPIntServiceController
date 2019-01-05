function SetNameOfSort(Element) {
    var text = document.getElementById("nameOfSort");
    if (Element.value == "byName") {
        text.innerHTML = "Сортировать: By Name";
    } else {
        text.innerHTML = "Сортировать: By Time";
    }
    return false;
}