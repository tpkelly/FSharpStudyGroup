module Exercise

open System.Drawing
open System
open Helpers

let rec generateTreeAtDepth(startX, startY, lineLength, angle, depth) =
    let trunk = { StartPoint= Point(startX,startY); EndPoint = Point(startX,startY + lineLength) }
    let child1 = { StartPoint= Point(trunk.EndPoint.X,trunk.EndPoint.Y); EndPoint = rotateWrtPoint (Point(trunk.EndPoint.X,trunk.EndPoint.Y + lineLength))  (Point(trunk.EndPoint.X,trunk.EndPoint.Y)) angle }
    let child2 = { StartPoint= Point(trunk.EndPoint.X,trunk.EndPoint.Y); EndPoint = rotateWrtPoint (Point(trunk.EndPoint.X,trunk.EndPoint.Y + lineLength))  (Point(trunk.EndPoint.X,trunk.EndPoint.Y)) -angle }
    match depth with
    | 0 -> seq {
            yield seq {yield trunk}
            yield [ child1 ; child2 ] |> List.toSeq
        }
    | _ -> Seq.concat [
            generateTreeAtDepth(child1.EndPoint.X, child1.EndPoint.Y, lineLength/2, angle, depth - 1);
            generateTreeAtDepth(child2.EndPoint.X, child2.EndPoint.Y, lineLength/2, angle, depth - 1)]

let generateTree(startX, startY, lineLength, angle) : seq<seq<Line>> = 
    Seq.initInfinite(fun index -> generateTreeAtDepth(startX, startY, lineLength, angle, index) |> Seq.concat)

let drawLine (graphics : Graphics) pen (line : Line) =
    graphics.DrawLine(pen,line.StartPoint,line.EndPoint)

let drawAndSaveFractalTree() = 
    let width = 1920
    let height = 1080
    let angle = 45.0
    let angleDegrees = angle * 1.0<degree> // Coax into the type float<degree>
    let lineLength = (int)((float)height / (2.0 * (1.0 + Math.Cos(angle * Math.PI / 180.0))))
    let bmp = new Bitmap(width,height)

    let blackPen = new Pen(Color.Black, 3.0f)
    
    use graphics = Graphics.FromImage(bmp)
    let drawLine' = drawLine graphics blackPen //You might be able to think of a better style for this. Think of this like mathematical derivation f(x) -> f'(x)
    generateTree(width/2, 0, lineLength, angleDegrees) |> Seq.take 10 |> Seq.concat |> Seq.iter drawLine'

    bmp.Save("..\\..\\FractalTree.jpeg")

            
    

