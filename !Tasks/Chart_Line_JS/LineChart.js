function createBarChart(data, width, height, color,MaxValueX,StepValueX, MaxValueY,StepValueY, NameX,NameY) {
    var maxX = Number.NEGATIVE_INFINITY;
    var maxY = Number.NEGATIVE_INFINITY;
    var MaxValueСonditionalX = width/MaxValueX;
    var MaxValueСonditionalY = height/MaxValueY;
    // создаем контейнер для диаграммы 
    // createElementNS первый параметр определяет пространство имен для элемента <svg xmlns="http://www.w3.org/2000/svg">
    var chart = document.createElementNS("http://www.w3.org/2000/svg", "svg");
    //chart.style.width = width;
    chart.style.width = width*MaxValueСonditionalX;
  //  var tmp= height+40;
  //var tmp= height+40;
    var tmp= height*MaxValueСonditionalY;// добавил 40 для того чтоб было куда текст стунуть
    // chart.style.height = height+40;
    chart.style.height = tmp;
  //  chart.setAttribute("viewBox", "0 0"+' ' +width + ' '+tmp); 
  chart.setAttribute("viewBox", "-40 0"+' ' +width*MaxValueСonditionalX + ' '+tmp); 

    // var myTitle = document.createElementNS("http://www.w3.org/2000/svg","text");
    // myTitle.setAttribute("y", 0);
    // myTitle.setAttribute("x", width /2);
    // myTitle.appendChild(document.createTextNode(title));

    // var line = chart.line(0, 100, 100, 0).move(20, 20)
    // line.stroke({ color: '#f06', width: 10, linecap: 'round' })
    //ось Y
    //  var lineX = document.createElementNS("http://www.w3.org/2000/svg", "line");
    //  lineX.setAttribute('id','lineX');
    //  lineX.setAttribute('x1',0);
    //  lineX.setAttribute('y1',0);
    //  lineX.setAttribute('x2',0);
    //  lineX.setAttribute('y2',height);
    //  lineX.setAttribute("stroke", "black");
    //  lineX.setAttribute("stroke-width",3);


    var lineY = document.createElementNS("http://www.w3.org/2000/svg", "line");
    lineY.setAttribute('id','lineY');
    lineY.setAttribute('x1',0);
    lineY.setAttribute('y1',0);
    lineY.setAttribute('x2',0);
    lineY.setAttribute('y2',height * MaxValueСonditionalY);
    lineY.setAttribute("stroke", "black");
    lineY.setAttribute("stroke-width",3);


    //ось X
    //  var lineX = document.createElementNS("http://www.w3.org/2000/svg", "line");
    //  lineX.setAttribute('id','lineY');
    //  lineX.setAttribute('x1',0);
    //  lineX.setAttribute('y1',height);
    //  lineX.setAttribute('x2',width);
    //  lineX.setAttribute('y2',height);
    //  lineX.setAttribute("stroke", "black");
    //  lineX.setAttribute("stroke-width",3);


       var lineX= document.createElementNS("http://www.w3.org/2000/svg", "line");
       lineX.setAttribute('id','lineX');
       lineX.setAttribute('x1',0);
       lineX.setAttribute('y1',height*MaxValueСonditionalY);
       lineX.setAttribute('x2',width*MaxValueСonditionalY);
       lineX.setAttribute('y2',height*MaxValueСonditionalY);
       lineX.setAttribute("stroke", "black");
       lineX.setAttribute("stroke-width",3);
    // myNumderText.setAttributeNS(null,"y",height - barHeight+barHeight/2);
    // var lineY = document.createElementNS("http://www.w3.org/2000/svg", "line");
    
    // var draw = SVG('drawing').size(300, 130)

    // var line = draw.line(0, 100, 100, 0).move(20, 20)
    // line.stroke({ color: '#f06', width: 10, linecap: 'round' })

    // находим максимальное значение в массиве данных
    // var max = Number.NEGATIVE_INFINITY;
    // for (var i = 0; i < data.length; i++) {
    //     if (max < data[i].Value) max = data[i].Value;
    // }


    // текст по Y
    var newTextY = document.createElementNS("http://www.w3.org/2000/svg","text");
    var myTextY= NameY;
    newTextY.setAttributeNS(null,"x",-39);
    newTextY.setAttributeNS(null,"y",20);
    newTextY.setAttributeNS(null,"width","100%");
    newTextY.setAttributeNS(null,"height","auto");
    // newTextY.setAttributeNS(null,"font-size","20");
    newTextY.appendChild(document.createTextNode(myTextY));

     // текст по X
     var newTextX = document.createElementNS("http://www.w3.org/2000/svg","text");
     var myTextX= NameX;
     newTextX.setAttributeNS(null,"x",width*MaxValueСonditionalX-80);
     newTextX.setAttributeNS(null,"y",tmp-1);// вот тут я уперся 
     newTextX.setAttributeNS(null,"width","100%");
     newTextY.setAttributeNS(null,"height","auto");
     // newTextY.setAttributeNS(null,"font-size","20");
     newTextX.appendChild(document.createTextNode(myTextX));

     // шкла Y 
     var TextSkaleY;
     for (let index = MaxValueСonditionalY*MaxValueY; index < tmp; index ++) {
       // const element = array[index];
       var TmpIndex=tmp%index;
       if (TmpIndex===0) {
        TextSkaleY = document.createElementNS("http://www.w3.org/2000/svg","text");
        var textSkaleY = tmp-index;
        TextSkaleY.setAttributeNS(null,"x",-10);
        TextSkaleY.setAttributeNS(null,"font-size","7");
        TextSkaleY.setAttributeNS(null,"width","100%");
        TextSkaleY.setAttributeNS(null,"height","auto");


        TextSkaleY.setAttributeNS(null,"y",textSkaleY);
        TextSkaleY.appendChild(document.createTextNode(textSkaleY/index+'-'));
        chart.appendChild(TextSkaleY);

       }
       
        
     }
     //TextSkaleY.appendChild(document.createTextNode(StepValueY+'-'));

   
    for (var i = 0; i < data.length; i++) {

        var dot=data[i].Value ;
        for (var i = 0; i < dot.length; i++){
        var X=dot[i].X;
        var Y=dot[i].Y;
        if (maxX < X) 
            {maxX = X}
        if (maxY < Y) 
            {maxY = Y}


        }

    }


    var scaleY = height / maxY;// сИметрия
    var barWidth = Math.floor(width / data.length);

  




    // создаем отдельный элемент диаграммы
    for (var i = 0; i < data.length; i++) {
        var points="",X,Y;
        var polyline = document.createElementNS("http://www.w3.org/2000/svg", "polyline");
        polyline.setAttribute('id','polyline-id'+i);
        // polyline.setAttribute('points',"");

        // var points = polyline.getAttribute("points");
        

        var dot=data[i].Value ;
        for (var j = 0; j < dot.length; j++){
         X=dot[j].X;
         Y=dot[j].Y;
         //Y=height-Y;
         Y=tmp-Y;
  
        if (j===dot.length-1)
        {
            points += X+','+Y;
        }
        else
        points += X+','+Y+' ';
        // points.setAttribute("points", points);
        // line.setAttribute("x", X,);
        // line.setAttribute("y", Y);

        }
        polyline.setAttribute('points',points);

       // var dotWidth=width-data[i].Value.X ;
        

        
       
        // var barHeight = data[i].Value * scaleX;
        var barHeight =Y * scaleY;
        // ?????
        // // polyline.setAttribute("height", barHeight + "px");
        // // polyline.setAttribute("width", barWidth - 4 + "px");

        // // polyline.setAttribute("y", height - barHeight);
        // // polyline.setAttribute("x", barWidth * i );
        //?


        polyline.style.stroke = color[i];
        polyline.style.fill="transparent";
        // var myNumderText = document.createElementNS("http://www.w3.org/2000/svg","text");
        // myNumderText.setAttributeNS(null,"x",barWidth * i+barWidth/2.2);
        // myNumderText.setAttributeNS(null,"y",height - barHeight+barHeight/2);
        // myNumderText.appendChild(document.createTextNode(data[i].Value));



        // var newText = document.createElementNS("http://www.w3.org/2000/svg","text");
        // var myText= data[i].Name;
        // newText.setAttributeNS(null,"x",barWidth * i+barWidth/2.5);
        // newText.setAttributeNS(null,"y",height+20);
        // newText.setAttributeNS(null,"width","100%");
        // newText.setAttributeNS(null,"height","auto");
        // newText.setAttributeNS(null,"font-size","20");
        // newText.appendChild(document.createTextNode(myText));


        // text.setAttribute("height", barHeight + "px");
        // text.setAttribute("width", barWidth - 4 + "px");

        // text.setAttribute("y", height - barHeight);
        // text.setAttribute("x", barWidth * i );

        polyline.addEventListener("mouseover", onOver);
        polyline.addEventListener("mouseout", onOut);

        chart.appendChild(polyline);
        // chart.appendChild(myNumderText);
        // chart.appendChild(newText);
        chart.appendChild(lineX);
        chart.appendChild(lineY);
        chart.appendChild(newTextY);
        chart.appendChild(newTextX);
        
    }
    var col;
    function onOver() { 
        col=this.style.stroke; 
        this.style.stroke = "red"; }
    function onOut() { 
        
        this.style.stroke = col; }

    return chart;
}