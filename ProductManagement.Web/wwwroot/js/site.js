document.addEventListener('DOMContentLoaded', function () {
    let options = { 'hover': true, 'closeOnClick': true };
    var elems = document.querySelectorAll('.dropdown-trigger');
    var instances = M.Sidenav.init(elems, options);
    console.log('hi');

    modalProduct = document.getElementById('modal1');
    triggerModalBtn = document.getElementById('modelTrigger');
    modalTriggerIndex = document.getElementById('modelTriggerIndex');
    let modalOptions = {};
    if (modalProduct != null) {
      
        modelInstance = M.Modal.init(modalProduct, modalOptions);
        if (triggerModalBtn != null) {
        console.log('loaded modal');

            triggerModalBtn.addEventListener('click', displayModal);
        }
    }

    let buttonsDelete = document.querySelectorAll('.btnDelete');
    if (modalTriggerIndex != null) {
        modalTriggerIndex.addEventListener('click', listenModal);
    }
    if (buttonsDelete != null) {
        modelInstance =  M.Modal.init(modalProduct, modalOptions);
        buttonsDelete.forEach(button =>
            button.addEventListener('click', displayText));
    }
});
var modalProduct;
var triggerModalBtn;
var modelInstance;
var modalTriggerIndex;
