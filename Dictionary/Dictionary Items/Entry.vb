Imports System.Xml.Serialization

''' <summary>
''' An Entry is a word along with it's language components, as they are laid out in a dictionary. 
''' The Entry is comprised of several key properties. The main parts of an Entry are: 
''' 1- the actual word it represents. 2- at least 1 Definition. 3- A Part Of Speech. 
''' Other properties are: 
''' 1- A Variant (A word which can be used interchangeably, often a different spelling). 2- Syllables in the word. 
''' 3- The word's Pronunciation using standardized symbols. 4- The Origin of the word.
''' An Entry is often comprised of more than one meaning, and sometimes the meanings are in a different Part Of Speech.
''' A Definition is being defined as a group of meanings belonging to a single Part Of Speach. 
''' Within that Definition, you can have more than one meaning. A meaning is kept in a Term for that Definition. 
''' A Term is comprised of the meaning, and a Note for that meaning (often used for a sample sentence of it's usage).
''' A Definition can have multiple Terms, and an Entry can have multiple Definitions. All properties of the same Entry for a particular word.
''' </summary>
''' <remarks></remarks>
<XmlRoot("Entry")> _
Public Class Entry
    Private _word As String
    Private _variant As String
    Private _syllables As String
    Private _pronunciation As String
    Private _definitions As List(Of Definition)
    Private _origin As String

    ''' <summary>
    ''' Creates a new instance of an Entry.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        _definitions = New List(Of Definition)
    End Sub

    ''' <summary>
    ''' Gets or Sets the word for this Entry.
    ''' </summary>
    ''' <value>A string value representing the word for this Entry.</value>
    ''' <returns>A string value representing the word for this Entry.</returns>
    ''' <remarks></remarks>
    <XmlAttribute(AttributeName:="Word")> _
    Public Property Word As String
        Get
            Return _word
        End Get
        Set(value As String)
            _word = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the Variant for the word belonging to this Entry.
    ''' </summary>
    ''' <value>A string value representing the variant word for this Entry.</value>
    ''' <returns>A string value representing the variant word for this Entry.</returns>
    ''' <remarks></remarks>
    <XmlAttribute(AttributeName:="Variant")> _
    Public Property VariantWord As String
        Get
            Return _variant
        End Get
        Set(value As String)
            _variant = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the string representation for the Syllables for the word belonging to this Entry.
    ''' </summary>
    ''' <value>A string value representing the Syllables in the word for this Entry.</value>
    ''' <returns>A string value representing the Syllables in the word for this Entry.</returns>
    ''' <remarks></remarks>
    <XmlElementAttribute(ElementName:="Syllables")> _
    Public Property Syllables As String
        Get
            Return _syllables
        End Get
        Set(value As String)
            _syllables = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the string representation for the Pronunciation for the word belonging to this Entry.
    ''' </summary>
    ''' <value>A string value representing the Pronunciation for the word in this Entry.</value>
    ''' <returns>A string value representing the Pronunciation for the word in this Entry.</returns>
    ''' <remarks></remarks>
    <XmlElementAttribute(ElementName:="Pronounciation")> _
    Public Property Pronunciation As String
        Get
            Return _pronunciation
        End Get
        Set(value As String)
            _pronunciation = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the string representation for the Origin of the word belonging to this Entry.
    ''' </summary>
    ''' <value>A string value representing the Origin for the word in this Entry.</value>
    ''' <returns>A string value representing the Origin for the word in this Entry.</returns>
    ''' <remarks></remarks>
    <XmlElementAttribute(ElementName:="Origin")> _
    Public Property Origin As String
        Get
            Return _origin
        End Get
        Set(value As String)
            _origin = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or Sets the list of Definition for the word belonging to this Entry.
    ''' </summary>
    ''' <value>A list of Definition for the word in this Entry.</value>
    ''' <returns>A list of Definition for the word in this Entry.</returns>
    ''' <remarks></remarks>
    <XmlArray(ElementName:="Definitions")> _
    Public Property Definitions As List(Of Definition)
        Get
            Return _definitions
        End Get
        Set(value As List(Of Definition))
            _definitions = value
        End Set
    End Property

    ''' <summary>
    ''' The word belonging to this Entry.
    ''' </summary>
    ''' <returns>The word belonging to this Entry.</returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        Return Me.Word
    End Function
End Class
