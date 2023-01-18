function createBarChart(data, width, height, color, title) {

    // создаем контейнер для диаграммы 
    // createElementNS первый параметр определяет пространство имен для элемента <svg xmlns="http://www.w3.org/2000/svg">
    var chart = document.createElementNS("http://www.w3.org/2000/svg", "svg");
    chart.style.width = width;
    chart.style.height = height+40;
    // var myTitle = document.createElementNS("http://www.w3.org/2000/svg","text");
    // myTitle.setAttribute("y", 0);
    // myTitle.setAttribute("x", width /2);
    // myTitle.appendChild(document.createTextNode(title));

    // var line = chart.line(0, 100, 100, 0).move(20, 20)
    // line.stroke({ color: '#f06', width: 10, linecap: 'round' })
    //ось X
     var lineX = document.createElementNS("http://www.w3.org/2000/svg", "line");
     lineX.setAttribute('id','lineX');
     lineX.setAttribute('x1',0);
     lineX.setAttribute('y1',0);
     lineX.setAttribute('x2',0);
     lineX.setAttribute('y2',height);
     lineX.setAttribute("stroke", "black");
     lineX.setAttribute("stroke-width",3);

    //ось y
     var lineY = document.createElementNS("http://www.w3.org/2000/svg", "line");
     lineY.setAttribute('id','lineY');
     lineY.setAttribute('x1',0);
     lineY.setAttribute('y1',height);
     lineY.setAttribute('x2',width);
     lineY.setAttribute('y2',height);
     lineY.setAttribute("stroke", "black");
     lineY.setAttribute("stroke-width",3);
    // myNumderText.setAttributeNS(null,"y",height - barHeight+barHeight/2);
    // var lineY = document.createElementNS("http://www.w3.org/2000/svg", "line");
    
    // var draw = SVG('drawing').size(300, 130)

    // var line = draw.line(0, 100, 100, 0).move(20, 20)
    // line.stroke({ color: '#f06', width: 10, linecap: 'round' })

    // находим максимальное значение в массиве данных
    var max = Number.NEGATIVE_INFINITY;
    for (var i = 0; i < data.length; i++) {
        if (max < data[i].Value) max = data[i].Value;
    }

    var scale = height / max;
    var barWidth = Math.floor(width / data.length);

    // создаем отдельный элемент диаграммы
    for (var i = 0; i < data.length; i++) {
        var bar = document.createElementNS("http://www.w3.org/2000/svg", "rect");

        
       
        var barHeight = data[i].Value * scale;
        bar.setAttribute("height", barHeight + "px");
        bar.setAttribute("width", barWidth - 4 + "px");

        bar.setAttribute("y", height - barHeight);
        bar.setAttribute("x", barWidth * i );

        bar.style.fill = color[i];
        var myNumderText = document.createElementNS("http://www.w3.org/2000/svg","text");
        myNumderText.setAttributeNS(null,"x",barWidth * i+barWidth/2.2);
        myNumderText.setAttributeNS(null,"y",height - barHeight+barHeight/2);
        myNumderText.appendChild(document.createTextNode(data[i].Value));



        var newText = document.createElementNS("http://www.w3.org/2000/svg","text");
        var myText= data[i].Name;
        newText.setAttributeNS(null,"x",barWidth * i+barWidth/2.5);
        newText.setAttributeNS(null,"y",height+20);
        newText.setAttributeNS(null,"width","100%");
        newText.setAttributeNS(null,"height","auto");
        newText.setAttributeNS(null,"font-size","20");
        newText.appendChild(document.createTextNode(myText));


        // text.setAttribute("height", barHeight + "px");
        // text.setAttribute("width", barWidth - 4 + "px");

        // text.setAttribute("y", height - barHeight);
        // text.setAttribute("x", barWidth * i );

        bar.addEventListener("mouseover", onOver);
         bar.addEventListener("mouseout", onOut);

        chart.appendChild(bar);
        chart.appendChild(myNumderText);
        chart.appendChild(newText);
        chart.appendChild(lineX);
        chart.appendChild(lineY);
      //  chart.appendChild(myTitle);
    }
    var col;
    function onOver() { 
        col=this.style.fill; 
        this.style.fill = "red"; }
    function onOut() { 
        
        this.style.fill = col; }

    return chart;
}