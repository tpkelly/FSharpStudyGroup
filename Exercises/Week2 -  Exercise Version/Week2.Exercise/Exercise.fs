module Exercise

open System.Drawing
open System
open Helpers

let generateTree() = 
    let trunk = { StartPoint= Point(1920/2,0); EndPoint = Point(1920/2,100) }
    let child1 = { StartPoint= Point(1920/2,100); EndPoint = rotateWrtPoint (Point(1920/2,200))  (Point(1920/2,100)) 45.0<degree> }
    let child2 = { StartPoint= Point(1920/2,100); EndPoint = rotateWrtPoint (Point(1920/2,200))  (Point(1920/2,100)) -45.0<degree> }
    seq {
        yield seq {yield trunk}
        yield [ child1 ; child2 ] |> List.toSeq
    }

let drawLine (graphics : Graphics) pen (line : Line) =
    graphics.DrawLine(pen,line.StartPoint,line.EndPoint)

let drawAndSaveFractalTree() = 
    let width = 1920
    let height = 1080

    let bmp = new Bitmap(width,height)

    let blackPen = new Pen(Color.Black, 3.0f)
    
    use graphics = Graphics.FromImage(bmp)
    let drawLine' = drawLine graphics blackPen //You might be able to think of a better style for this. Think of this like mathematical derivation f(x) -> f'(x)
    generateTree() |> Seq.take 2 |> Seq.concat |> Seq.iter drawLine'

    bmp.Save("..\\..\\FractalTree.jpeg")

            
    

