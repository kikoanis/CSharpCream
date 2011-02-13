using System;
using System.Collections;
using System.Drawing;

namespace Cream
{
    /// <summary>
    /// This interface should be implemented by any class whose instances are intended 
    /// to be executed by a thread.
    /// </summary>
    public interface IThreadRunnable
    {
        /// <summary>
        /// This method has to be implemented in order that starting of the thread causes the object's 
        /// run method to be called in that separately executing thread.
        /// </summary>
        void Run();
    }

    /// <summary>
    /// Contains conversion support Elements such as classes, interfaces and static methods.
    /// </summary>
    public class SupportClass
    {
        /// <summary>
        /// This class provides functionality not found in .NET collection-related interfaces.
        /// </summary>
        public class CollectionSupport
        {
            /// <summary>
            /// Adds a new Element to the specified collection.
            /// </summary>
            /// <param name="c">Collection where the new Element will be added.</param>
            /// <param name="obj">Object to Add.</param>
            /// <returns>true</returns>
            public static bool Add(ICollection c, Object obj)
            {
                bool added = false;
                //Reflection. Invoke either the "Add" or "Add" method.
                System.Reflection.MethodInfo method;
                try
                {
                    //Get the "Add" method for proprietary classes
                    method = c.GetType().GetMethod("Add") ?? c.GetType().GetMethod("Add");
                    var index = (int) method.Invoke(c, new[] {obj});
                    if (index >=0)	
                        added = true;
                }
                catch (Exception e)
                {
                    throw e;
                }
                return added;
            }

            /// <summary>
            /// Adds all of the Elements of the "c" collection to the "target" collection.
            /// </summary>
            /// <param name="target">Collection where the new Elements will be added.</param>
            /// <param name="c">Collection whose Elements will be added.</param>
            /// <returns>Returns true if at least one Element was added, false otherwise.</returns>
            public static bool AddAll(ICollection target, ICollection c)
            {
                IEnumerator e = new ArrayList(c).GetEnumerator();
                bool added = false;

                //Reflection. Invoke "addAll" method for proprietary classes
                System.Reflection.MethodInfo method;
                try
                {
                    method = target.GetType().GetMethod("addAll");

                    if (method != null)
                        added = (bool) method.Invoke(target, new Object[] {c});
                    else
                    {
                        method = target.GetType().GetMethod("Add");
                        while (e.MoveNext())
                        {
                            bool tempBAdded =  (int) method.Invoke(target, new[] {e.Current}) >= 0;
                            added = added ? true : tempBAdded;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return added;
            }

            /// <summary>
            /// Removes all the Elements from the collection.
            /// </summary>
            /// <param name="c">The collection to Remove Elements.</param>
            public static void Clear(ICollection c)
            {
                //Reflection. Invoke "Clear" method or "clear" method for proprietary classes
                System.Reflection.MethodInfo method;
                try
                {
                    method = c.GetType().GetMethod("Clear") ?? c.GetType().GetMethod("clear");

                    method.Invoke(c, new Object[] {});
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            /// <summary>
            /// Determines whether the collection Contains the specified Element.
            /// </summary>
            /// <param name="c">The collection to check.</param>
            /// <param name="obj">The object to locate in the collection.</param>
            /// <returns>true if the Element is in the collection.</returns>
            public static bool Contains(ICollection c, Object obj)
            {
                bool contains;

                //Reflection. Invoke "Contains" method for proprietary classes
                System.Reflection.MethodInfo method;
                try
                {
                    method = c.GetType().GetMethod("Contains") ?? c.GetType().GetMethod("Contains");

                    contains = (bool)method.Invoke(c, new[] {obj});
                }
                catch (Exception e)
                {
                    throw e;
                }

                return contains;
            }

            /// <summary>
            /// Determines whether the collection Contains all the Elements in the specified collection.
            /// </summary>
            /// <param name="target">The collection to check.</param>
            /// <param name="c">Collection whose Elements would be checked for containment.</param>
            /// <returns>true id the target collection Contains all the Elements of the specified collection.</returns>
            public static bool ContainsAll(ICollection target, ICollection c)
            {						
                IEnumerator e =  c.GetEnumerator();

                bool contains = false;

                //Reflection. Invoke "containsAll" method for proprietary classes or "Contains" method for each Element in the collection
                System.Reflection.MethodInfo method;
                try
                {
                    method = target.GetType().GetMethod("containsAll");

                    if (method != null)
                        contains = (bool)method.Invoke(target, new Object[] {c});
                    else
                    {					
                        method = target.GetType().GetMethod("Contains");
                        while (e.MoveNext())
                        {
                            if ((contains = (bool)method.Invoke(target, new[] {e.Current})) == false)
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return contains;
            }

            /// <summary>
            /// Removes the specified Element from the collection.
            /// </summary>
            /// <param name="c">The collection where the Element will be removed.</param>
            /// <param name="obj">The Element to Remove from the collection.</param>
            public static bool Remove(ICollection c, Object obj)
            {
                bool changed = false;

                //Reflection. Invoke "Remove" method for proprietary classes or "Remove" method
                System.Reflection.MethodInfo method;
                try
                {
                    method = c.GetType().GetMethod("Remove");

                    if (method != null)
                        method.Invoke(c, new[] {obj});
                    else
                    {
                        method = c.GetType().GetMethod("Contains");
                        changed = (bool)method.Invoke(c, new[] {obj});
                        method = c.GetType().GetMethod("Remove");
                        method.Invoke(c, new[] {obj});
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                return changed;
            }

            /// <summary>
            /// Removes all the Elements from the specified collection that are contained in the target collection.
            /// </summary>
            /// <param name="target">Collection where the Elements will be removed.</param>
            /// <param name="c">Elements to Remove from the target collection.</param>
            /// <returns>true</returns>
            public static bool RemoveAll(ICollection target, ICollection c)
            {
                ArrayList al = ToArrayList(c);
                IEnumerator e = al.GetEnumerator();

                //Reflection. Invoke "removeAll" method for proprietary classes or "Remove" for each Element in the collection
                System.Reflection.MethodInfo method;
                try
                {
                    method = target.GetType().GetMethod("removeAll");

                    if (method != null)
                        method.Invoke(target, new Object[] {al});
                    else
                    {
                        method = target.GetType().GetMethod("Remove");
                        System.Reflection.MethodInfo methodContains = target.GetType().GetMethod("Contains");

                        while (e.MoveNext())
                        {
                            while ((bool) methodContains.Invoke(target, new[] {e.Current}))
                                method.Invoke(target, new[] {e.Current});
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return true;
            }

            /// <summary>
            /// Retains the Elements in the target collection that are contained in the specified collection
            /// </summary>
            /// <param name="target">Collection where the Elements will be removed.</param>
            /// <param name="c">Elements to be retained in the target collection.</param>
            /// <returns>true</returns>
            public static bool RetainAll(ICollection target, ICollection c)
            {
                IEnumerator e = new ArrayList(target).GetEnumerator();
                var al = new ArrayList(c);

                //Reflection. Invoke "retainAll" method for proprietary classes or "Remove" for each Element in the collection
                System.Reflection.MethodInfo method;
                try
                {
                    method = c.GetType().GetMethod("retainAll");

                    if (method != null)
                        method.Invoke(target, new Object[] {c});
                    else
                    {
                        method = c.GetType().GetMethod("Remove");

                        while (e.MoveNext())
                        {
                            if (al.Contains(e.Current) == false)
                                method.Invoke(target, new[] {e.Current});
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return true;
            }

            /// <summary>
            /// Returns an array containing all the Elements of the collection.
            /// </summary>
            /// <returns>The array containing all the Elements of the collection.</returns>
            public static Object[] ToArray(ICollection c)
            {	
                int index = 0;
                var objects = new Object[c.Count];
                IEnumerator e = c.GetEnumerator();

                while (e.MoveNext())
                    objects[index++] = e.Current;

                return objects;
            }

            /// <summary>
            /// Obtains an array containing all the Elements of the collection.
            /// </summary>
            /// <param name="objects">The array into which the Elements of the collection will be stored.</param>
            /// <param name="c">The array into which the Elements of the collection will be stored.</param>
            /// <returns>The array containing all the Elements of the collection.</returns>
            public static Object[] ToArray(ICollection c, Object[] objects)
            {	
                int index = 0;

                Type type = objects.GetType().GetElementType();
                var objs = (Object[]) Array.CreateInstance(type, c.Count );

                IEnumerator e = c.GetEnumerator();

                while (e.MoveNext())
                    objs[index++] = e.Current;

                //If objects is smaller than c then do not return the new array in the parameter
                if (objects.Length >= c.Count)
                    objs.CopyTo(objects, 0);

                return objs;
            }

            /// <summary>
            /// Converts an ICollection instance to an ArrayList instance.
            /// </summary>
            /// <param name="c">The ICollection instance to be converted.</param>
            /// <returns>An ArrayList instance in which its Elements are the Elements of the ICollection instance.</returns>
            public static ArrayList ToArrayList(ICollection c)
            {
                var tempArrayList = new ArrayList();
                IEnumerator tempEnumerator = c.GetEnumerator();
                while (tempEnumerator.MoveNext())
                    tempArrayList.Add(tempEnumerator.Current);
                return tempArrayList;
            }
        }


        /*******************************/
        //Provides access to a static System.Random class instance
        static public Random Random = new Random();

        /*******************************/
        /// <summary>
        /// This method returns the literal value received
        /// </summary>
        /// <param name="literal">The literal to return</param>
        /// <returns>The received value</returns>
        public static long Identity(long literal)
        {
            return literal;
        }

        /// <summary>
        /// This method returns the literal value received
        /// </summary>
        /// <param name="literal">The literal to return</param>
        /// <returns>The received value</returns>
        public static ulong Identity(ulong literal)
        {
            return literal;
        }

        /// <summary>
        /// This method returns the literal value received
        /// </summary>
        /// <param name="literal">The literal to return</param>
        /// <returns>The received value</returns>
        public static float Identity(float literal)
        {
            return literal;
        }

        /// <summary>
        /// This method returns the literal value received
        /// </summary>
        /// <param name="literal">The literal to return</param>
        /// <returns>The received value</returns>
        public static double Identity(double literal)
        {
            return literal;
        }

        /*******************************/
        /// <summary>
        /// Class used to store and retrieve an object command specified as a String.
        /// </summary>
        public class CommandManager
        {
            /// <summary>
            /// Private Hashtable used to store objects and their commands.
            /// </summary>
            private static readonly Hashtable Commands = new Hashtable();

            /// <summary>
            /// Sets a command to the specified object.
            /// </summary>
            /// <param name="obj">The object that has the command.</param>
            /// <param name="cmd">The command for the object.</param>
            public static void SetCommand(Object obj, String cmd)
            {
                if (obj != null)
                {
                    if (Commands.Contains(obj))
                        Commands[obj] = cmd;
                    else
                        Commands.Add(obj, cmd);
                }
            }

            /// <summary>
            /// Gets a command associated with an object.
            /// </summary>
            /// <param name="obj">The object whose command is going to be retrieved.</param>
            /// <returns>The command of the specified object.</returns>
            public static String GetCommand(Object obj)
            {
                String result = "";
                if (obj != null)
                    result = Convert.ToString(Commands[obj]);
                return result;
            }



            /// <summary>
            /// Checks if the Control Contains a command, if it does not it sets the default
            /// </summary>
            /// <param name="button">The control whose command will be checked</param>
            public static void CheckCommand(System.Windows.Forms.ButtonBase button)
            {
                if (button != null)
                {
                    if (GetCommand(button).Equals(""))
                        SetCommand(button, button.Text);
                }
            }

            /// <summary>
            /// Checks if the Control Contains a command, if it does not it sets the default
            /// </summary>
            /// <param name="menuItem">The control whose command will be checked</param>
            public static void CheckCommand(System.Windows.Forms.MenuItem menuItem)
            {
                if (menuItem != null)
                {
                    if (GetCommand(menuItem).Equals(""))
                        SetCommand(menuItem, menuItem.Text);
                }
            }

            /// <summary>
            /// Checks if the Control Contains a command, if it does not it sets the default
            /// </summary>
            /// <param name="comboBox">The control whose command will be checked</param>
            public static void CheckCommand(System.Windows.Forms.ComboBox comboBox)
            {
                if (comboBox != null)
                {
                    if (GetCommand(comboBox).Equals(""))
                        SetCommand(comboBox,"comboBoxChanged");
                }
            }

        }
        /*******************************/
        /// <summary>
        /// Give functions to obtain information of graphic Elements
        /// </summary>
        public class GraphicsManager
        {
            //Instance of GDI+ drawing surfaces graphics hashtable
            static public GraphicsHashTable Manager = new GraphicsHashTable();

            /// <summary>
            /// Creates a new Graphics object from the device context handle associated with the Graphics
            /// parameter
            /// </summary>
            /// <param name="oldGraphics">Graphics instance to obtain the parameter from</param>
            /// <returns>A new GDI+ drawing surface</returns>
            public static Graphics CreateGraphics(Graphics oldGraphics)
            {
                IntPtr hdc = oldGraphics.GetHdc();
                Graphics createdGraphics = Graphics.FromHdc(hdc);
                oldGraphics.ReleaseHdc(hdc);
                return createdGraphics;
            }

            /// <summary>
            /// This method draws a Bezier curve.
            /// </summary>
            /// <param name="graphics">It receives the Graphics instance</param>
            /// <param name="array">An array of (x,y) pairs of coordinates used to draw the curve.</param>
            public static void Bezier(Graphics graphics, int[] array)
            {
                Pen pen = Manager.GetPen(graphics);
                try
                {
                    graphics.DrawBezier(pen, array[0], array[1], array[2], array[3], array[4], array[5], array[6], array[7]);
                }
                catch(IndexOutOfRangeException e)
                {
                    throw new IndexOutOfRangeException(e.ToString());
                }
            }

            /// <summary>
            /// Gets the text Size width and height from a given GDI+ drawing surface and a given font
            /// </summary>
            /// <param name="graphics">Drawing surface to use</param>
            /// <param name="graphicsFont">Font type to measure</param>
            /// <param name="text">String of text to measure</param>
            /// <returns>A point structure with both Size dimentions; x for width and y for height</returns>
            public static Point GetTextSize(Graphics graphics, Font graphicsFont, String text)
            {
                SizeF tempSizeF = graphics.MeasureString(text, graphicsFont);
                var textSize = new Point {X = (int) tempSizeF.Width, Y = (int) tempSizeF.Height};
                return textSize;
            }

            /// <summary>
            /// Gets the text Size width and height from a given GDI+ drawing surface and a given font
            /// </summary>
            /// <param name="graphics">Drawing surface to use</param>
            /// <param name="graphicsFont">Font type to measure</param>
            /// <param name="text">String of text to measure</param>
            /// <param name="width">Maximum width of the string</param>
            /// <param name="format">StringFormat object that represents formatting information, such as line spacing, for the string</param>
            /// <returns>A point structure with both Size dimentions; x for width and y for height</returns>
            public static Point GetTextSize(Graphics graphics, Font graphicsFont, String text, Int32 width, StringFormat format)
            {
                SizeF tempSizeF = graphics.MeasureString(text, graphicsFont, width, format);
                var textSize = new Point {X = (int) tempSizeF.Width, Y = (int) tempSizeF.Height};
                return textSize;
            }

            /// <summary>
            /// Gives functionality over a hashtable of GDI+ drawing surfaces
            /// </summary>
            public class GraphicsHashTable:Hashtable 
            {
                /// <summary>
                /// Gets the graphics object from the given control
                /// </summary>
                /// <param name="control">Control to obtain the graphics from</param>
                /// <returns>A graphics object with the control's characteristics</returns>
                public Graphics GetGraphics(System.Windows.Forms.Control control)
                {
                    Graphics graphic;
                    if (control.Visible)
                    {
                        graphic = control.CreateGraphics();
                        SetColor(graphic, control.ForeColor);
                        SetFont(graphic, control.Font);
                    }
                    else
                    {
                        graphic = null;
                    }
                    return graphic;
                }

                /// <summary>
                /// Sets the background color property to the given graphics object in the hashtable. If the Element doesn't exist, then it adds the graphic Element to the hashtable with the given background color.
                /// </summary>
                /// <param name="graphic">Graphic Element to search or Add</param>
                /// <param name="color">Background color to set</param>
                public void SetBackColor(Graphics graphic, Color color)
                {
                    if (this[graphic] != null)
                        ((GraphicsProperties) this[graphic]).BackColor = color;
                    else
                    {
                        var tempProps = new GraphicsProperties {BackColor = color};
                        Add(graphic, tempProps);
                    }
                }

                /// <summary>
                /// Gets the background color property to the given graphics object in the hashtable. If the Element doesn't exist, then it returns White.
                /// </summary>
                /// <param name="graphic">Graphic Element to search</param>
                /// <returns>The background color of the graphic</returns>
                public Color GetBackColor(Graphics graphic)
                {
                    if (this[graphic] == null)
                        return Color.White;
                    return ((GraphicsProperties) this[graphic]).BackColor;
                }

                /// <summary>
                /// Sets the text color property to the given graphics object in the hashtable. If the Element doesn't exist, then it adds the graphic Element to the hashtable with the given text color.
                /// </summary>
                /// <param name="graphic">Graphic Element to search or Add</param>
                /// <param name="color">Text color to set</param>
                public void SetTextColor(Graphics graphic, Color color)
                {
                    if (this[graphic] != null)
                        ((GraphicsProperties) this[graphic]).TextColor = color;
                    else
                    {
                        var tempProps = new GraphicsProperties {TextColor = color};
                        Add(graphic, tempProps);
                    }
                }

                /// <summary>
                /// Gets the text color property to the given graphics object in the hashtable. If the Element doesn't exist, then it returns White.
                /// </summary>
                /// <param name="graphic">Graphic Element to search</param>
                /// <returns>The text color of the graphic</returns>
                public Color GetTextColor(Graphics graphic)
                {
                    if (this[graphic] == null)
                        return Color.White;
                    return ((GraphicsProperties) this[graphic]).TextColor;
                }

                /// <summary>
                /// Sets the GraphicBrush property to the given graphics object in the hashtable. If the Element doesn't exist, then it adds the graphic Element to the hashtable with the given GraphicBrush.
                /// </summary>
                /// <param name="graphic">Graphic Element to search or Add</param>
                /// <param name="brush">GraphicBrush to set</param>
                public void SetBrush(Graphics graphic, SolidBrush brush) 
                {
                    if (this[graphic] != null)
                        ((GraphicsProperties) this[graphic]).GraphicBrush = brush;
                    else
                    {
                        var tempProps = new GraphicsProperties {GraphicBrush = brush};
                        Add(graphic, tempProps);
                    }
                }
			
                /// <summary>
                /// Sets the GraphicBrush property to the given graphics object in the hashtable. If the Element doesn't exist, then it adds the graphic Element to the hashtable with the given GraphicBrush.
                /// </summary>
                /// <param name="graphic">Graphic Element to search or Add</param>
                /// <param name="brush">GraphicBrush to set</param>
                public void SetPaint(Graphics graphic, Brush brush) 
                {
                    if (this[graphic] != null)
                        ((GraphicsProperties) this[graphic]).PaintBrush = brush;
                    else
                    {
                        var tempProps = new GraphicsProperties {PaintBrush = brush};
                        Add(graphic, tempProps);
                    }
                }
			
                /// <summary>
                /// Sets the GraphicBrush property to the given graphics object in the hashtable. If the Element doesn't exist, then it adds the graphic Element to the hashtable with the given GraphicBrush.
                /// </summary>
                /// <param name="graphic">Graphic Element to search or Add</param>
                /// <param name="color">Color to set</param>
                public void SetPaint(Graphics graphic, Color color) 
                {
                    Brush brush = new SolidBrush(color);
                    if (this[graphic] != null)
                        ((GraphicsProperties) this[graphic]).PaintBrush = brush;
                    else
                    {
                        var tempProps = new GraphicsProperties {PaintBrush = brush};
                        Add(graphic, tempProps);
                    }
                }


                /// <summary>
                /// Gets the HatchBrush property to the given graphics object in the hashtable. If the Element doesn't exist, then it returns Blank.
                /// </summary>
                /// <param name="graphic">Graphic Element to search</param>
                /// <returns>The HatchBrush setting of the graphic</returns>
                public System.Drawing.Drawing2D.HatchBrush GetBrush(Graphics graphic)
                {
                    if (this[graphic] == null)
                        return new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Plaid,Color.Black,Color.Black);
                    return new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Plaid,((GraphicsProperties) this[graphic]).GraphicBrush.Color,((GraphicsProperties) this[graphic]).GraphicBrush.Color);
                }

                /// <summary>
                /// Gets the HatchBrush property to the given graphics object in the hashtable. If the Element doesn't exist, then it returns Blank.
                /// </summary>
                /// <param name="graphic">Graphic Element to search</param>
                /// <returns>The Brush setting of the graphic</returns>
                public Brush GetPaint(Graphics graphic)
                {
                    if (this[graphic] == null)
                        return new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.Plaid,Color.Black,Color.Black);
                    return ((GraphicsProperties) this[graphic]).PaintBrush;
                }

                /// <summary>
                /// Sets the GraphicPen property to the given graphics object in the hashtable. If the Element doesn't exist, then it adds the graphic Element to the hashtable with the given Pen.
                /// </summary>
                /// <param name="graphic">Graphic Element to search or Add</param>
                /// <param name="pen">Pen to set</param>
                public void SetPen(Graphics graphic, Pen pen) 
                {
                    if (this[graphic] != null)
                        ((GraphicsProperties) this[graphic]).GraphicPen = pen;
                    else
                    {
                        var tempProps = new GraphicsProperties {GraphicPen = pen};
                        Add(graphic, tempProps);
                    }
                }

                /// <summary>
                /// Gets the GraphicPen property to the given graphics object in the hashtable. If the Element doesn't exist, then it returns Black.
                /// </summary>
                /// <param name="graphic">Graphic Element to search</param>
                /// <returns>The GraphicPen setting of the graphic</returns>
                public Pen GetPen(Graphics graphic)
                {
                    if (this[graphic] == null)
                        return Pens.Black;
                    return ((GraphicsProperties) this[graphic]).GraphicPen;
                }

                /// <summary>
                /// Sets the GraphicFont property to the given graphics object in the hashtable. If the Element doesn't exist, then it adds the graphic Element to the hashtable with the given Font.
                /// </summary>
                /// <param name="graphic">Graphic Element to search or Add</param>
                /// <param name="font">Font to set</param>
                public void SetFont(Graphics graphic, Font font) 
                {
                    if (this[graphic] != null)
                        ((GraphicsProperties) this[graphic]).GraphicFont = font;
                    else
                    {
                        var tempProps = new GraphicsProperties {GraphicFont = font};
                        Add(graphic,tempProps);
                    }
                }

                /// <summary>
                /// Gets the GraphicFont property to the given graphics object in the hashtable. If the Element doesn't exist, then it returns Microsoft Sans Serif with Size 8.25.
                /// </summary>
                /// <param name="graphic">Graphic Element to search</param>
                /// <returns>The GraphicFont setting of the graphic</returns>
                public Font GetFont(Graphics graphic)
                {
                    if (this[graphic] == null)
                        return new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    return ((GraphicsProperties) this[graphic]).GraphicFont;
                }

                /// <summary>
                /// Sets the color properties for a given Graphics object. If the Element doesn't exist, then it adds the graphic Element to the hashtable with the color properties set with the given value.
                /// </summary>
                /// <param name="graphic">Graphic Element to search or Add</param>
                /// <param name="color">Color value to set</param>
                public void SetColor(Graphics graphic, Color color) 
                {
                    if (this[graphic] != null)
                    {
                        ((GraphicsProperties) this[graphic]).GraphicPen.Color = color;
                        ((GraphicsProperties) this[graphic]).GraphicBrush.Color = color;
                        ((GraphicsProperties) this[graphic]).Color = color;
                    }
                    else
                    {
                        var tempProps = new GraphicsProperties
                                            {
                                                GraphicPen = {Color = color},
                                                GraphicBrush = {Color = color},
                                                Color = color
                                            };
                        Add(graphic,tempProps);
                    }
                }

                /// <summary>
                /// Gets the color property to the given graphics object in the hashtable. If the Element doesn't exist, then it returns Black.
                /// </summary>
                /// <param name="graphic">Graphic Element to search</param>
                /// <returns>The color setting of the graphic</returns>
                public Color GetColor(Graphics graphic)
                {
                    if (this[graphic] == null)
                        return Color.Black;
                    return ((GraphicsProperties) this[graphic]).Color;
                }

                /// <summary>
                /// This method gets the TextBackgroundColor of a Graphics instance
                /// </summary>
                /// <param name="graphic">The graphics instance</param>
                /// <returns>The color value in ARGB encoding</returns>
                public Color GetTextBackgroundColor(Graphics graphic)
                {
                    if (this[graphic] == null)
                        return Color.Black;
                    return ((GraphicsProperties) this[graphic]).TextBackgroundColor;
                }

                /// <summary>
                /// This method set the TextBackgroundColor of a Graphics instace
                /// </summary>
                /// <param name="graphic">The graphics instace</param>
                /// <param name="color">The System.Color to set the TextBackgroundColor</param>
                public void SetTextBackgroundColor(Graphics graphic, Color color) 
                {
                    if (this[graphic] != null)
                    {
                        ((GraphicsProperties) this[graphic]).TextBackgroundColor = color;								
                    }
                    else
                    {
                        var tempProps = new GraphicsProperties {TextBackgroundColor = color};
                        Add(graphic,tempProps);
                    }
                }

                /// <summary>
                /// Structure to store properties from System.Drawing.Graphics objects
                /// </summary>
                class GraphicsProperties
                {
                    public Color TextBackgroundColor = Color.Black;
                    public Color Color = Color.Black;
                    public Color BackColor = Color.White;
                    public Color TextColor = Color.Black;
                    public SolidBrush GraphicBrush = new SolidBrush(Color.Black);
                    public Brush PaintBrush = new SolidBrush(Color.Black);
                    public Pen   GraphicPen = new Pen(Color.Black);
                    public Font  GraphicFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
                }
            }
        }

        /*******************************/
        /// <summary>
        /// Support class used to handle threads
        /// </summary>
        public class ThreadClass : IThreadRunnable
        {
            /// <summary>
            /// The instance of System.Threading.Thread
            /// </summary>
            private System.Threading.Thread _threadField;
	      
            /// <summary>
            /// Initializes a new instance of the ThreadClass class
            /// </summary>
            public ThreadClass()
            {
                _threadField = new System.Threading.Thread(Run);
            }
	 
            /// <summary>
            /// Initializes a new instance of the Thread class.
            /// </summary>
            /// <param name="name">The name of the thread</param>
            public ThreadClass(String name)
            {
                _threadField = new System.Threading.Thread(Run);
                Name = name;
            }
	      
            /// <summary>
            /// Initializes a new instance of the Thread class.
            /// </summary>
            /// <param name="start">A ThreadStart delegate that references the methods to be invoked when this thread begins executing</param>
            public ThreadClass(System.Threading.ThreadStart start)
            {
                _threadField = new System.Threading.Thread(start);
            }
	 
            /// <summary>
            /// Initializes a new instance of the Thread class.
            /// </summary>
            /// <param name="start">A ThreadStart delegate that references the methods to be invoked when this thread begins executing</param>
            /// <param name="name">The name of the thread</param>
            public ThreadClass(System.Threading.ThreadStart start, String name)
            {
                _threadField = new System.Threading.Thread(start);
                Name = name;
            }
	      
            /// <summary>
            /// This method has no functionality unless the method is overridden
            /// </summary>
            public virtual void Run()
            {
            }
	      
            /// <summary>
            /// Causes the operating system to change the state of the current thread instance to ThreadState.Running
            /// </summary>
            public virtual void Start()
            {
                _threadField.Start();
            }
	      
            /// <summary>
            /// Interrupts a thread that is in the WaitSleepJoin thread state
            /// </summary>
            public virtual void Interrupt()
            {
                _threadField.Interrupt();
            }
	      
            /// <summary>
            /// Gets the current thread instance
            /// </summary>
            public System.Threading.Thread Instance
            {
                get
                {
                    return _threadField;
                }
                set
                {
                    _threadField = value;
                }
            }
	      
            /// <summary>
            /// Gets or sets the name of the thread
            /// </summary>
            public String Name
            {
                get
                {
                    return _threadField.Name;
                }
                set
                {
                    if (_threadField.Name == null)
                        _threadField.Name = value; 
                }
            }
	      
            /// <summary>
            /// Gets or sets a value indicating the scheduling priority of a thread
            /// </summary>
            public System.Threading.ThreadPriority Priority
            {
                get
                {
                    return _threadField.Priority;
                }
                set
                {
                    _threadField.Priority = value;
                }
            }
	      
            /// <summary>
            /// Gets a value indicating the execution status of the current thread
            /// </summary>
            public bool IsAlive
            {
                get
                {
                    return _threadField.IsAlive;
                }
            }
	      
            /// <summary>
            /// Gets or sets a value indicating whether or not a thread is a background thread.
            /// </summary>
            public bool IsBackground
            {
                get
                {
                    return _threadField.IsBackground;
                } 
                set
                {
                    _threadField.IsBackground = value;
                }
            }
	      
            /// <summary>
            /// Blocks the calling thread until a thread terminates
            /// </summary>
            public void Join()
            {
                _threadField.Join();
            }
	      
            /// <summary>
            /// Blocks the calling thread until a thread terminates or the specified time elapses
            /// </summary>
            /// <param name="miliSeconds">Time of wait in milliseconds</param>
            public void Join(long miliSeconds)
            {
                lock(this)
                {
                    _threadField.Join(new TimeSpan(miliSeconds * 10000));
                }
            }
	      
            /// <summary>
            /// Blocks the calling thread until a thread terminates or the specified time elapses
            /// </summary>
            /// <param name="miliSeconds">Time of wait in milliseconds</param>
            /// <param name="nanoSeconds">Time of wait in nanoseconds</param>
            public void Join(long miliSeconds, int nanoSeconds)
            {
                lock(this)
                {
                    _threadField.Join(new TimeSpan(miliSeconds * 10000 + nanoSeconds * 100));
                }
            }
	      
            /// <summary>
            /// Resumes a thread that has been suspended
            /// </summary>
            public void Resume()
            {
#pragma warning disable 612,618
                _threadField.Resume();
#pragma warning restore 612,618
            }
	      
            /// <summary>
            /// Raises a ThreadAbortException in the thread on which it is invoked, 
            /// to begin the process of terminating the thread. Calling this method 
            /// usually terminates the thread
            /// </summary>
            public void Abort()
            {
                _threadField.Abort();
            }
	      
            /// <summary>
            /// Raises a ThreadAbortException in the thread on which it is invoked, 
            /// to begin the process of terminating the thread while also providing
            /// exception information about the thread termination. 
            /// Calling this method usually terminates the thread.
            /// </summary>
            /// <param name="stateInfo">An object that Contains application-specific information, such as state, which can be used by the thread being aborted</param>
            public void Abort(Object stateInfo)
            {
                lock(this)
                {
                    _threadField.Abort(stateInfo);
                }
            }
	      
            /// <summary>
            /// Suspends the thread, if the thread is already suspended it has no effect
            /// </summary>
            public void Suspend()
            {
                _threadField.Suspend();
            }
	      
            /// <summary>
            /// Obtain a String that represents the current Object
            /// </summary>
            /// <returns>A String that represents the current Object</returns>
            public override String ToString()
            {
                return "Thread[" + Name + "," + Priority + "," + "" + "]";
            }
	     
            /// <summary>
            /// Gets the currently running thread
            /// </summary>
            /// <returns>The currently running thread</returns>
            public static ThreadClass Current()
            {
                var currentThread = new ThreadClass {Instance = System.Threading.Thread.CurrentThread};
                return currentThread;
            }
        }


        /*******************************/
        /// <summary>
        /// SupportClass for the Stack class.
        /// </summary>
        public class StackSupport
        {
            /// <summary>
            /// Removes the Element at the top of the stack and returns it.
            /// </summary>
            /// <param name="stack">The stack where the Element at the top will be returned and removed.</param>
            /// <returns>The Element at the top of the stack.</returns>
            public static Object Pop(ArrayList stack)
            {
                Object obj = stack[stack.Count - 1];
                stack.RemoveAt(stack.Count - 1);

                return obj;
            }
        }


    }
}