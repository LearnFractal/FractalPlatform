const daysTag = document.querySelector(".days"),
currentDate = document.querySelector(".current-date"),
prevNextIcon = document.querySelectorAll(".icons span"),
daysTagId=document.getElementById("daysTagId");


// получение новой даты, текущего года и месяца
let date = new Date(),
currYear = date.getFullYear(),
currMonth = date.getMonth(),
currDay=date.getDate();


// сохранение полного имени всех месяцев в массиве
const months = ["January", "February", "March", "April", "May", "June", "July",
              "August", "September", "October", "November", "December"];

const renderCalendar = () => {
    let firstDayofMonth = new Date(currYear, currMonth, 1).getDay(), // получение первого дня месяца
    lastDateofMonth = new Date(currYear, currMonth + 1, 0).getDate(), // получение последней даты месяца
    lastDayofMonth = new Date(currYear, currMonth, lastDateofMonth).getDay(), //получение последнего дня месяца
    lastDateofLastMonth = new Date(currYear, currMonth, 0).getDate(); // получение последней даты предыдущего месяца
    let liTag = "";

    for (let i = firstDayofMonth; i > 0; i--) { // создание ли предыдущего месяца последние дни
        liTag += `<li class="inactive">${lastDateofLastMonth - i + 1}</li>`;
    }

    for (let i = 1; i <= lastDateofMonth; i++) { // создание ли всех дней текущего месяца
        // добавляем активный класс в li, если текущий день, месяц и год совпадают
        let isToday = i === date.getDate() && currMonth === new Date().getMonth() 
                     && currYear === new Date().getFullYear() ? "active" : "";
                    //  alert(isToday);
        liTag += `<li class="${isToday}" id=dayId${i} onclick="selectElement(dayId${i})">${i}</li>`;
        
        // liTag += `<li class="">${i}</li>`;
    }

    for (let i = lastDayofMonth; i < 6; i++) { // создание ли первых дней следующего месяца
        liTag += `<li class="inactive">${i - lastDayofMonth + 1}</li>`
        //alert(liTag);
    }
    currentDate.innerText = `${currDay} ${months[currMonth]} ${currYear} `; //date.getDate()}  передача текущего понедельника и года как текста currentDate text
    daysTag.innerHTML = liTag;
    // liTag.addEventListener("click", () => {

    // });

    
}
renderCalendar();
//daysTagId.addEventListener
//<ul class="days"></ul>
function selectElement(element){
    alert(element.id );// получение id  єемента для дальнейшей работы с ним 
   // let index  = tags.indexOf(tag);
    // tags = [...tags.slice(0, index), ...tags.slice(index + 1)];
    // element.parentElement.remove();
    // countTags();
}

prevNextIcon.forEach(icon => { // получение предыдущей и следующей значков
    icon.addEventListener("click", () => { // добавление события клика на обе иконки
        // если щелкнутый значок является предыдущим значком, то уменьшите текущий месяц на 1, иначе увеличу его на 1
        currMonth = icon.id === "prev" ? currMonth - 1 : currMonth + 1;

        if(currMonth < 0 || currMonth > 11) { // если текущий месяц меньше 0 или больше 11
            // создание новой даты текущего года и месяца и передача ее как значения даты
            date = new Date(currYear, currMonth, currDay);
            currYear = date.getFullYear(); // обновление текущего года с новой датой года
            currMonth = date.getMonth(); // обновление текущего месяца с новой датой месяца
           // alert(currDay);
            currDay=date.getDay()+1;
        } else {
            date = new Date(); //  передать текущую дату как значение даты
        }
        renderCalendar(); //вызов функции renderCalendar
    });
});