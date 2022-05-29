var button = document.querySelector('.div2');

function clickHandler(e) {    // eventHandler써주면 자동으로 들어오지만 활용하려면 e를 써주기.  e는 이벤트 객체로써, 발생한 이벤트에 대한 많은 정보를 담고 있는 객체.
    console.log(e.target);    // event 객체의 타겟
    console.log(e.currentTarget);
    console.log(this);   // this는 menu이다. menu가 addEventLisner를 호출하였으므로
    console.log(this == e.currentTarget);
}

button.addEventListener('click', clickHandler);
