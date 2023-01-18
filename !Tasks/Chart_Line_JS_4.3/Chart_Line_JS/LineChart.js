function createBarChart(data, width, height, color,MaxValueX,StepValueX, MaxValueY,StepValueY, NameX,NameY) {
    var maxX = Number.NEGATIVE_INFINITY;
    var maxY = Number.NEGATIVE_INFINITY;
    var MaxValueСonditionalX = width/MaxValueX;
    var MaxValueСonditionalY = height/MaxValueY;
    // создаем контейнер для диаграммы 
    // createElementNS первый параметр определяет пространство имен для элемента <svg xmlns="http://www.w3.org/2000/svg">
    var chart = document.createElementNS("http://www.w3.org/2000/svg", "svg");
    //chart.style.width = width;
    var tmpX=width*MaxValueСonditionalX;
    var tmpX1=tmpX+54;
    chart.style.width = tmpX+55;
  //  var tmp= height+40;
  //var tmp= height+40;
    var tmpY= height*MaxValueСonditionalY;// добавил 40 для того чтоб было куда текст стунуть
    // chart.style.height = height+40;
    chart.style.height = tmpY+40;
    var tmpY1= tmpY+40;
  //  chart.setAttribute("viewBox", "0 0"+' ' +width + ' '+tmp); 
  chart.setAttribute("viewBox", "-40 -7"+' ' +tmpX1+ ' '+tmpY1 ); 


    //ось Y
   

    var lineY = document.createElementNS("http://www.w3.org/2000/svg", "line");
    lineY.setAttribute('id','lineY');
    lineY.setAttribute('x1',0);
    lineY.setAttribute('y1',-2);
    lineY.setAttribute('x2',0);
    lineY.setAttribute('y2',height * MaxValueСonditionalY);
    lineY.setAttribute("stroke", "black");
    lineY.setAttribute("stroke-width",3);


    //ось X

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
    
    newTextY.appendChild(document.createTextNode(myTextY));

     // текст по X
     var newTextX = document.createElementNS("http://www.w3.org/2000/svg","text");
     var myTextX= NameX;
     newTextX.setAttributeNS(null,"x",width*MaxValueСonditionalX-80);
     newTextX.setAttributeNS(null,"y",tmpY+15);
     newTextX.setAttributeNS(null,"width","100%");
     newTextY.setAttributeNS(null,"height","auto");
     
     newTextX.appendChild(document.createTextNode(myTextX));

     // шкла Y 
     var TextSkaleY;
    

    for (let index = StepValueY; index <= MaxValueY; index +=StepValueY) {
       
         TextSkaleY = document.createElementNS("http://www.w3.org/2000/svg","text");
         
         var textSkaleY = tmpY-(index *MaxValueСonditionalY);//MaxValueСonditionalY==3

        var oneElementY=tmpY/MaxValueY;

         TextSkaleY.setAttributeNS(null,"x",-10);
         TextSkaleY.setAttributeNS(null,"font-size","7");
         TextSkaleY.setAttributeNS(null,"width","100%");
         TextSkaleY.setAttributeNS(null,"height","auto");
 
         TextSkaleY.setAttributeNS(null,"y",tmpY-(oneElementY *index));
         TextSkaleY.appendChild(document.createTextNode(index+'-'));
         chart.appendChild(TextSkaleY);

        
     }
     


    // шкла X
    var TextSkaleX;
    

    for (let index = StepValueX; index <= MaxValueX; index +=StepValueX) {
       
        TextSkaleX = document.createElementNS("http://www.w3.org/2000/svg","text");

        var oneElementX=tmpX/MaxValueX;

        TextSkaleX.setAttributeNS(null,"x",oneElementX *index);
        TextSkaleX.setAttributeNS(null,"font-size","7");
        TextSkaleX.setAttributeNS(null,"width","100%");
        TextSkaleX.setAttributeNS(null,"height","auto");
        TextSkaleX.setAttributeNS(null,"y",tmpY+10);
        TextSkaleX.appendChild(document.createTextNode(index));
         chart.appendChild(TextSkaleX);

     }

     var TextSkaleX1;
    

     for (let indexX1 = StepValueX; indexX1 <= MaxValueX; indexX1 +=StepValueX) {
        
         TextSkaleX1 = document.createElementNS("http://www.w3.org/2000/svg","text");
 
         var oneElementX1=tmpX/MaxValueX;
 
         TextSkaleX1.setAttributeNS(null,"x",oneElementX1 *indexX1);
         TextSkaleX1.setAttributeNS(null,"font-size","10");
         TextSkaleX1.setAttributeNS(null,"width","100%");
         TextSkaleX1.setAttributeNS(null,"height","auto");
         TextSkaleX1.setAttributeNS(null,"y",tmpY+3);
         TextSkaleX1.appendChild(document.createTextNode('|'));
        chart.appendChild(TextSkaleX1);
 
      }
   
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
         Y=tmpY-Y;
  
        if (j===dot.length-1)
        {
            points += X+','+Y;
        }
        else
        points += X+','+Y+' ';
       

        }
        polyline.setAttribute('points',points);

        var barHeight =Y * scaleY;
        // ?????
        // // polyline.setAttribute("height", barHeight + "px");
        // // polyline.setAttribute("width", barWidth - 4 + "px");

        // // polyline.setAttribute("y", height - barHeight);
        // // polyline.setAttribute("x", barWidth * i );
        //?


        polyline.style.stroke = color[i];
        polyline.style.fill="transparent";
        

        polyline.addEventListener("mouseover", onOver);
        polyline.addEventListener("mouseout", onOut);

        chart.appendChild(polyline);

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