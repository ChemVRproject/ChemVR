using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.EventSystems;
using System.Threading;
using System.Runtime.InteropServices;

// VR

public class ReadFile : MonoBehaviour
{
	
	public GameObject classbox;
	
	// атомы
	public GameObject thePrefab;
	
	// связи
	public GameObject thePrefabSingleLink;
	public GameObject thePrefabDoubleLink;
	public GameObject thePrefabTripleLink;
	public GameObject ruller;
	
	// настройки отображения
	int ViewAtom=0;
	int SkyBox=0;
	
	//материалы атомов
	public Material White;
	public Material Red;
	public Material Blue;
	public Material Black;
		
	public Material FWhite;
	public Material FRed;
	public Material FBlue;
	public Material FBlack;
	
	public Material FWhite1;
	public Material FRed1;
	public Material FBlue1;
	public Material FBlack1;
	
	public Material FWhite2;
	public Material FRed2;
	public Material FBlue2;
	public Material FBlack2;
	
	// материалы фона
	public Material SkyB;
	public Material SkyR;
	public Material SkyC;
	public Material SkyWhite;
	public Material SkyBlack;
	public Material SkyGrey;
	
	// кнопки
	public GameObject buttonquant;	
	
	public Slider frameslider;
	public TMPro.TMP_Text framenumber;
	public TMPro.TMP_Text textforgraph;	
	
	private bool feature = false;
	
	// адрес файла
	string PathF;
	string FileName;
	
	// фреймы
	int MaxFrameNum = 1000;
	int FrameNumber = 1;
	int Number;
	
	// массивы с информацией
	List<GameObject> generatedObjects = new List<GameObject>();
	List<GameObject> selectedObjects = new List<GameObject>();
	List<GameObject> generatedStatistic = new List<GameObject>();
	List<GameObject> generatedCylinder = new List<GameObject>();
    
	float [,] masAtomfloat;
	string [,] masAtomstr;
	float [,] masLinksfloat;
	
	
	// для взаимодействия с атомами
	public GameObject Inform;
	public GameObject InformChoosen;
	public GameObject Inform2;	
	public GameObject Inform2text;
	public GameObject FileInform;
	private int selected = 0;
	private RaycastHit hit;
	public GameObject lastselObj;
	private Material MatNorm = null;
	
	//для нового алгоритма
	private float koefficient = 1.15f;
	
	private int [,] valentnost = new int [96, 7] 
	{
		{ 1, 0, 0, 0, 0, 0, 0},						
		{ 0, 0, 0, 0, 0, 0, 0},						
		{ 1, 0, 0, 0, 0, 0, 0},
		{ 2, 0, 0, 0, 0, 0, 0},		
		{ 3, 0, 0, 0, 0, 0, 0},
		{ 4, 0, 0, 0, 0, 0, 0},	
		{ 3, 0, 0, 0, 0, 0, 0},	
		{ 2, 0, 0, 0, 0, 0, 0},	
		{ 1, 0, 0, 0, 0, 0, 0},						
		{ 0, 0, 0, 0, 0, 0, 0},						
		{ 1, 0, 0, 0, 0, 0, 0},		
		{ 2, 0, 0, 0, 0, 0, 0},		
		{ 3, 0, 0, 0, 0, 0, 0},
		{ 4, 0, 0, 0, 0, 0, 0},							
		{ 3, 5, 0, 0, 0, 0, 0},	
		{ 2, 6, 0, 0, 0, 0, 0},		
		{ 1, 3, 5, 7, 0, 0, 0},			
		{ 0, 0, 0, 0, 0, 0, 0},					
		{ 1, 0, 0, 0, 0, 0, 0},
		{ 2, 0, 0, 0, 0, 0, 0},		
		{ 3, 0, 0, 0, 0, 0, 0},		
		{ 2, 3, 5, 0, 0, 0, 0},	
		{ 2, 3, 4, 5, 0, 0, 0},	
		{ 2, 3, 7, 0, 0, 0, 0},	
		{ 2, 3, 4, 6, 7, 0, 0},	
		{ 2, 3, 0, 0, 0, 0, 0},	
		{ 2, 3, 0, 0, 0, 0, 0},	
		{ 2, 3, 4, 0, 0, 0, 0},			
		{ 1, 2, 0, 0, 0, 0, 0},
		{ 2, 0, 0, 0, 0, 0, 0},		
		{ 3, 0, 0, 0, 0, 0, 0},	
		{ 2, 4, 0, 0, 0, 0, 0},	
		{ 3, 5, 0, 0, 0, 0, 0},	
		{ 2, 4, 6, 0, 0, 0, 0},	
		{ 1, 3, 5, 7, 0, 0, 0},	
		{ 2, 4, 6, 0, 0, 0, 0},	
		{ 1, 0, 0, 0, 0, 0, 0},		
		{ 2, 0, 0, 0, 0, 0, 0},		
		{ 3, 0, 0, 0, 0, 0, 0},	
		{ 2, 3, 4, 0, 0, 0, 0},			
		{ 1, 2, 3, 4, 5, 0, 0},	
		{ 2, 3, 4, 5, 6, 0, 0},			
		{ 1, 2, 3, 4, 5, 6, 7},	
		{ 2, 3, 4, 5, 6, 7, 8},	
		{ 1, 2, 3, 4, 5, 0, 0},	
		{ 4, 2, 3, 1, 0, 0, 0},			
		{ 2, 1, 3, 0, 0, 0, 0},	
		{ 2, 0, 0, 0, 0, 0, 0},	
		{ 3, 0, 0, 0, 0, 0, 0},		
		{ 2, 4, 0, 0, 0, 0, 0},	
		{ 3, 5, 0, 0, 0, 0, 0},	
		{ 6, 4, 2, 0, 0, 0, 0},	
		{ 1, 3, 5, 7, 0, 0, 0},			
		{ 2, 4, 6, 7, 0, 0, 0},	
		{ 1, 0, 0, 0, 0, 0, 0},	
		{ 2, 0, 0, 0, 0, 0, 0},	
		{ 3, 0, 0, 0, 0, 0, 0},	
		{ 3, 4, 0, 0, 0, 0, 0},	
		{ 3, 4, 0, 0, 0, 0, 0},
		{ 3, 0, 0, 0, 0, 0, 0},	
		{ 3, 0, 0, 0, 0, 0, 0},	
		{ 2, 3, 0, 0, 0, 0, 0},
		{ 2, 3, 0, 0, 0, 0, 0},			
		{ 3, 0, 0, 0, 0, 0, 0},	
		{ 3, 4, 0, 0, 0, 0, 0},	
		{ 3, 0, 0, 0, 0, 0, 0},	
		{ 3, 0, 0, 0, 0, 0, 0},	
		{ 3, 0, 0, 0, 0, 0, 0},		
		{ 2, 3, 0, 0, 0, 0, 0},
		{ 2, 3, 0, 0, 0, 0, 0},			
		{ 3, 0, 0, 0, 0, 0, 0},					
		{ 2, 3, 4, 0, 0, 0, 0},			
		{ 1, 2, 3, 4, 5, 0, 0},	
		{ 2, 3, 4, 5, 6, 0, 0},			
		{ 1, 2, 3, 4, 5, 6, 7},	
		{ 2, 3, 4, 5, 6, 8, 0},	
		{ 1, 2, 3, 4, 5, 6, 0},	
		{ 4, 2, 5, 3, 1, 0, 0},	
		{ 1, 2, 3, 0, 0, 0, 0},	
		{ 2, 0, 0, 0, 0, 0, 0},			
		{ 1, 3, 0, 0, 0, 0, 0},	
		{ 2, 4, 0, 0, 0, 0, 0},	
		{ 3, 5, 0, 0, 0, 0, 0},	
		{ 6, 4, 2, 0, 0, 0, 0},	
		{ 0, 0, 0, 0, 0, 0, 0},	
		{ 0, 0, 0, 0, 0, 0, 0},	
		{ 1, 0, 0, 0, 0, 0, 0},	
		{ 2, 0, 0, 0, 0, 0, 0},	
		{ 3, 0, 0, 0, 0, 0, 0},	
		{ 2, 3, 4, 0, 0, 0, 0},	
		{ 4, 5, 0, 0, 0, 0, 0},		
		{ 3, 4, 0, 0, 0, 0, 0},	
		{ 3, 4, 5, 6, 0, 0, 0},	
		{ 2, 3, 4, 0, 0, 0, 0},	
		{ 3, 4, 5, 6, 0, 0, 0},	
		{ 3, 4, 0, 0, 0, 0, 0}
	}; 
	
	private Dictionary<string, int> atomnumber = new Dictionary<string, int>
	{
		{ "H",	1},
		{ "He",	2},
		{ "Li",	3},
		{ "Be",	4},
		{ "B",	5},
		{ "C",	6},
		{ "N",	7},
		{ "O",	8},
		{ "F",	9},
		{ "Ne",	10},
		{ "Na",	11},
		{ "Mg",	12},
		{ "Al",	13},
		{ "Si",	14},
		{ "P",	15},
		{ "S",	16},
		{ "Cl",	17},
		{ "Ar",	18},
		{ "K",	19},
		{ "Ca",	20},
		{ "Sc",	21},
		{ "Ti",	22},
		{ "V",	23},
		{ "Cr",	24},
		{ "Mn",	25},
		{ "Fe",	26},
		{ "Co",	27},
		{ "Ni",	28},
		{ "Cu",	29},
		{ "Zn",	30},
		{ "Ga",	31},
		{ "Ge",	32},
		{ "As",	33},
		{ "Se",	34},
		{ "Br",	35},
		{ "Kr",	36},
		{ "Rb",	37},
		{ "Sr",	38},
		{ "Y",	39},
		{ "Zr",	40},
		{ "Nb",	41},
		{ "Mo",	42},
		{ "Tc",	43},
		{ "Ru",	44},
		{ "Rh",	45},
		{ "Pd",	46},
		{ "Ag",	47},
		{ "Cd",	48},
		{ "In",	49},
		{ "Sn",	50},
		{ "Sb",	51},
		{ "Te",	52},
		{ "I",	53},
		{ "Xe",	54},
		{ "Cs",	55},
		{ "Ba",	56},
		{ "La",	57},
		{ "Ce",	58},
		{ "Pr",	59},
		{ "Nd",	60},
		{ "Pm",	61},
		{ "Sm",	62},
		{ "Eu",	63},
		{ "Gd",	64},
		{ "Tb",	65},
		{ "Dy",	66},
		{ "Ho",	67},
		{ "Er",	68},
		{ "Tm",	69},
		{ "Yb",	70},
		{ "Lu",	71},
		{ "Hf",	72},
		{ "Ta",	73},
		{ "W",	74},
		{ "Re",	75},
		{ "Os",	76},
		{ "Ir",	77},
		{ "Pt",	78},
		{ "Au",	79},
		{ "Hg",	80},
		{ "Tl",	81},
		{ "Pb",	82},
		{ "Bi",	83},
		{ "Po",	84},
		{ "At",	85},
		{ "Rn",	86},
		{ "Fr",	87},
		{ "Ra",	88},
		{ "Ac",	89},
		{ "Th",	90},
		{ "Pa",	91},
		{ "U",	92},
		{ "Np",	93},
		{ "Pu",	94},
		{ "Am",	95},
		{ "Cm",	96}

	};
	
	private Dictionary<string, int> covalentradius = new Dictionary<string, int>
	{
		{ "H",	31},
		{ "He",	28},
		{ "Li",	128},
		{ "Be",	96},
		{ "B",	84},
		{ "C",	76}, //радиус взят за модуль
		{ "N",	71},
		{ "O",	66},
		{ "F",	57},
		{ "Ne",	58},
		{ "Na",	166},
		{ "Mg",	141},
		{ "Al",	121},
		{ "Si",	111},
		{ "P",	107},
		{ "S",	105},
		{ "Cl",	102},
		{ "Ar",	106},
		{ "K",	203},
		{ "Ca",	176},
		{ "Sc",	170},
		{ "Ti",	160},
		{ "V",	153},
		{ "Cr",	139},
		{ "Mn",	139},
		{ "Fe",	132},
		{ "Co",	126},
		{ "Ni",	124},
		{ "Cu",	132},
		{ "Zn",	122},
		{ "Ga",	122},
		{ "Ge",	120},
		{ "As",	119},
		{ "Se",	120},
		{ "Br",	120},
		{ "Kr",	116},
		{ "Rb",	220},
		{ "Sr",	195},
		{ "Y",	190},
		{ "Zr",	175},
		{ "Nb",	164},
		{ "Mo",	154},
		{ "Tc",	147},
		{ "Ru",	146},
		{ "Rh",	142},
		{ "Pd",	139},
		{ "Ag",	145},
		{ "Cd",	144},
		{ "In",	142},
		{ "Sn",	139},
		{ "Sb",	139},
		{ "Te",	138},
		{ "I",	139},
		{ "Xe",	140},
		{ "Cs",	244},
		{ "Ba",	215},
		{ "La",	207},
		{ "Ce",	204},
		{ "Pr",	203},
		{ "Nd",	201},
		{ "Pm",	199},
		{ "Sm",	198},
		{ "Eu",	198},
		{ "Gd",	196},
		{ "Tb",	194},
		{ "Dy",	192},
		{ "Ho",	192},
		{ "Er",	189},
		{ "Tm",	190},
		{ "Yb",	187},
		{ "Lu",	187},
		{ "Hf",	175},
		{ "Ta",	170},
		{ "W",	162},
		{ "Re",	151},
		{ "Os",	144},
		{ "Ir",	141},
		{ "Pt",	136},
		{ "Au",	136},
		{ "Hg",	132},
		{ "Tl",	145},
		{ "Pb",	146},
		{ "Bi",	148},
		{ "Po",	140},
		{ "At",	150},
		{ "Rn",	150},
		{ "Fr",	260},
		{ "Ra",	221},
		{ "Ac",	215},
		{ "Th",	206},
		{ "Pa",	200},
		{ "U",	196},
		{ "Np",	190},
		{ "Pu",	187},
		{ "Am",	180},
		{ "Cm",	169}

	};
	
	private UnityEngine.Color[] atomColor = new UnityEngine.Color[96]
	{ 
	new Vector4(1f, 1f, 1f, 1f),
	new Vector4(0.850980392f,	1f,	1f, 1f),
	new Vector4(0.8f,	0.501960784f,	1f, 1f),
	new Vector4(0.760784314f,	1f,	0f, 1f),
	new Vector4(1f,	0.709803922f,	0.709803922f, 1f),
	new Vector4(0.564705882f,	0.564705882f,	0.564705882f, 1f),
	new Vector4(0.188235294f,	0.31372549f,	0.97254902f, 1f),
	new Vector4(1f,	0.050980392f,	0.050980392f, 1f),
	new Vector4(0.564705882f,	0.878431373f,	0.31372549f, 1f),
	new Vector4(0.701960784f,	0.890196078f,	0.960784314f, 1f),
	new Vector4(0.670588235f,	0.360784314f,	0.949019608f, 1f),
	new Vector4(0.541176471f,	1f,	0f, 1f),
	new Vector4(0.749019608f,	0.650980392f,	0.650980392f, 1f),
	new Vector4(0.941176471f,	0.784313725f,	0.62745098f, 1f),
	new Vector4(1f,	0.501960784f,	0f, 1f),
	new Vector4(1f,	1f,	0.188235294f, 1f),
	new Vector4(0.121568627f,	0.941176471f,	0.121568627f, 1f),
	new Vector4(0.501960784f,	0.819607843f,	0.890196078f, 1f),
	new Vector4(0.560784314f,	0.250980392f,	0.831372549f, 1f),
	new Vector4(0.239215686f,	1f,	0f, 1f),
	new Vector4(0.901960784f,	0.901960784f,	0.901960784f, 1f),
	new Vector4(0.749019608f,	0.760784314f,	0.780392157f, 1f),
	new Vector4(0.650980392f,	0.650980392f,	0.670588235f, 1f),
	new Vector4(0.541176471f,	0.6f,	0.780392157f, 1f),
	new Vector4(0.611764706f,	0.478431373f,	0.780392157f, 1f),
	new Vector4(0.878431373f,	0.4f,	0.2f, 1f),
	new Vector4(0.941176471f,	0.564705882f,	0.62745098f, 1f),
	new Vector4(0.31372549f,	0.815686275f,	0.31372549f, 1f),
	new Vector4(0.784313725f,	0.501960784f,	0.2f, 1f),
	new Vector4(0.490196078f,	0.501960784f,	0.690196078f, 1f),
	new Vector4(0.760784314f,	0.560784314f,	0.560784314f, 1f),
	new Vector4(0.4f,	0.560784314f,	0.560784314f, 1f),
	new Vector4(0.741176471f,	0.501960784f,	0.890196078f, 1f),
	new Vector4(1f,	0.631372549f,	0f, 1f),
	new Vector4(0.650980392f,	0.160784314f,	0.160784314f, 1f),
	new Vector4(0.360784314f,	0.721568627f,	0.819607843f, 1f),
	new Vector4(0.439215686f,	0.180392157f,	0.690196078f, 1f),
	new Vector4(0f,	1f,	0f, 1f),
	new Vector4(0.580392157f,	1f,	1f, 1f),
	new Vector4(0.580392157f,	0.878431373f,	0.878431373f, 1f),
	new Vector4(0.450980392f,	0.760784314f,	0.788235294f, 1f),
	new Vector4(0.329411765f,	0.709803922f,	0.709803922f, 1f),
	new Vector4(0.231372549f,	0.619607843f,	0.619607843f, 1f),
	new Vector4(0.141176471f,	0.560784314f,	0.560784314f, 1f),
	new Vector4(0.039215686f,	0.490196078f,	0.549019608f, 1f),
	new Vector4(0f,	0.411764706f,	0.521568627f, 1f),
	new Vector4(0.752941176f,	0.752941176f,	0.752941176f, 1f),
	new Vector4(1f,	0.850980392f,	0.560784314f, 1f),
	new Vector4(0.650980392f,	0.458823529f,	0.450980392f, 1f),
	new Vector4(0.4f,	0.501960784f,	0.501960784f, 1f),
	new Vector4(0.619607843f,	0.388235294f,	0.709803922f, 1f),
	new Vector4(0.831372549f,	0.478431373f,	0f, 1f),
	new Vector4(0.580392157f,	0f,	0.580392157f, 1f),
	new Vector4(0.258823529f,	0.619607843f,	0.690196078f, 1f),
	new Vector4(0.341176471f,	0.090196078f,	0.560784314f, 1f),
	new Vector4(0f,	0.788235294f,	0f, 1f),
	new Vector4(0.439215686f,	0.831372549f,	1f, 1f),
	new Vector4(1f,	1f,	0.780392157f, 1f),
	new Vector4(0.850980392f,	1f,	0.780392157f, 1f),
	new Vector4(0.780392157f,	1f,	0.780392157f, 1f),
	new Vector4(0.639215686f,	1f,	0.780392157f, 1f),
	new Vector4(0.560784314f,	1f,	0.780392157f, 1f),
	new Vector4(0.380392157f,	1f,	0.780392157f, 1f),
	new Vector4(0.270588235f,	1f,	0.780392157f, 1f),
	new Vector4(0.188235294f,	1f,	0.780392157f, 1f),
	new Vector4(0.121568627f,	1f,	0.780392157f, 1f),
	new Vector4(0f,	1f,	0.611764706f, 1f),
	new Vector4(0f,	0.901960784f,	0.458823529f, 1f),
	new Vector4(0f,	0.831372549f,	0.321568627f, 1f),
	new Vector4(0f,	0.749019608f,	0.219607843f, 1f),
	new Vector4(0f,	0.670588235f,	0.141176471f, 1f),
	new Vector4(0.301960784f,	0.760784314f,	1f, 1f),
	new Vector4(0.301960784f,	0.650980392f,	1f, 1f),
	new Vector4(0.129411765f,	0.580392157f,	0.839215686f, 1f),
	new Vector4(0.149019608f,	0.490196078f,	0.670588235f, 1f),
	new Vector4(0.149019608f,	0.4f,	0.588235294f, 1f),
	new Vector4(0.090196078f,	0.329411765f,	0.529411765f, 1f),
	new Vector4(0.815686275f,	0.815686275f,	0.878431373f, 1f),
	new Vector4(1f,	0.819607843f,	0.137254902f, 1f),
	new Vector4(0.721568627f,	0.721568627f,	0.815686275f, 1f),
	new Vector4(0.650980392f,	0.329411765f,	0.301960784f, 1f),
	new Vector4(0.341176471f,	0.349019608f,	0.380392157f, 1f),
	new Vector4(0.619607843f,	0.309803922f,	0.709803922f, 1f),
	new Vector4(0.670588235f,	0.360784314f,	0f, 1f),
	new Vector4(0.458823529f,	0.309803922f,	0.270588235f, 1f),
	new Vector4(0.258823529f,	0.509803922f,	0.588235294f, 1f),
	new Vector4(0.258823529f,	0f,	0.4f, 1f),
	new Vector4(0f,	0.490196078f,	0f, 1f),
	new Vector4(0.439215686f,	0.670588235f,	0.980392157f, 1f),
	new Vector4(0f,	0.729411765f,	1f, 1f),
	new Vector4(0f,	0.631372549f,	1f, 1f),
	new Vector4(0f,	0.560784314f,	1f, 1f),
	new Vector4(0f,	0.501960784f,	1f, 1f),
	new Vector4(0f,	0.419607843f,	1f, 1f),
	new Vector4(0.329411765f,	0.360784314f,	0.949019608f, 1f),
	new Vector4(0.470588235f,	0.360784314f,	0.890196078f, 1f)
	};
	
	// для камеры	
	public Camera mainCamera;
	public GameObject rightController;
	public Rigidbody body;
	public GameObject centreye;
	public BoxCollider boxCollider;
	
	public GameObject buttonMD;
	public GameObject buttonQ;
	private int formatfile = 2;
	
	[SerializeField] public Sprite circleSprite;
	[SerializeField] public Sprite cubeSprite;
	public RectTransform graphContainerIR;
	public GameObject IRspectrum;
	public RectTransform graphContainerStatistic;
	public GameObject Statistic;
	
	private int PlayFrame = 0;
	private Timer timer1; 
	
	public TMPro.TMP_Text selectedatom;
	public TMPro.TMP_Text lastselectedatom;
	private string RecordF;
	private int variant=1;
	
	void Start()
	{
		body.freezeRotation = true;
		body.useGravity = false;
		body.mass = 0.1f;
		body.drag = 10;
		System.Random rnd = new System.Random();
		int data  = rnd.Next(100, 999);
		RecordF = "/Result" + data.ToString() + ".txt";
		FileInfo fi = new FileInfo(Application.persistentDataPath + RecordF);
		fi.Create();
		OnButtonDownXYZ(1);
		SetBoxColliderSize();
    }	
   
	void readFirstFrameXYZ () //во время первого чтения мы создаем атомы и связи
	{	
		try
		{	
			using (StreamReader sr = new StreamReader(PathF))
			{
				
				string n = sr.ReadLine();
                Number = int.Parse(n); //число атомов
                int linesToSkip = ((Number+2)*(FrameNumber-1));
				
                for (int i = 0; i <= linesToSkip; ++i)
				{	
					sr.ReadLine(); //лишние строки
				}
				 
				masAtomfloat = new float [Number, 4]; //числовые данные про атом
				masAtomstr = new string [Number, 2]; //буквенные данные про атом
				masLinksfloat = new float [Number, 26]; //список связей атомов, для старого алгоритма - 17
				
                for (int i=0;i<Number;i++)
                {
                    string newstr = sr.ReadLine();
                    string[] massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
					masAtomstr[i,0]=massive[0];
					
					if (feature==true)
					{
						massive[1] = massive[1].Replace('.', ',');
						massive[2] = massive[2].Replace('.', ',');
						massive[3] = massive[3].Replace('.', ',');
					} 
					
					masAtomfloat[i,1] = float.Parse(massive[1]);
					masAtomfloat[i,2] = float.Parse(massive[2]);
					masAtomfloat[i,3] = float.Parse(massive[3]);
					
					GameObject newAtom = Instantiate<GameObject>(thePrefab);	
					newAtom.name = massive[0]+"_"+i;
					Vector3 iVector = new Vector3 (masAtomfloat[i,1], masAtomfloat[i,2], masAtomfloat[i,3]);
					newAtom.transform.position = iVector;
					float koef = (((covalentradius[massive[0]]/76.0f)-1.0f)/1.5f)+1.0f; //высчитываем размер атома относительно углерода
					newAtom.transform.localScale = new Vector3(koef, koef, koef);
					newAtom.GetComponent<Renderer>().material.color = atomColor[atomnumber[massive[0]]-1];
					generatedObjects.Add(newAtom);
					findLinks(i, iVector, massive[0]);
					//newAtom.GetComponent<BoxCollider>().size=newAtom.transform.localScale;
                }
				countLinks();
				drawLinks();
			}
		}
		catch (Exception e)
		{
			Console.WriteLine("The process failed: {0}", e.ToString());
		}
		return;  
	}
	
	void readFirstFrameOUT () //во время первого чтения мы создаем атомы и связи
	{	
		int linesToSkip =0;
		try
		{	
			using (StreamReader sr = new StreamReader(PathF))
			{	
				while (sr.ReadLine()!= "CARTESIAN COORDINATES (ANGSTROEM)")
				{
					linesToSkip++;
				} 
				linesToSkip -= 8;
			}	
		}
		catch (Exception e)
		{
			Console.WriteLine("The process failed: {0}", e.ToString());
		}		
		
		try
		{	
			using (StreamReader sr = new StreamReader(PathF))
			{
                for (int i = 0; i <= linesToSkip; ++i)
				{	
					sr.ReadLine(); //лишние строки
				}
				string newstr = sr.ReadLine();
                string[] massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
				Number = int.Parse(massive[4]);//число атомов
				masAtomfloat = new float [Number, 4]; //числовые данные про атом
				masAtomstr = new string [Number, 2]; //буквенные данные про атом
				masLinksfloat = new float [Number, 26]; //список связей атомов, для старого алгоритма - 17
				
				while (sr.ReadLine()!= "                 *** FINAL ENERGY EVALUATION AT THE STATIONARY POINT ***")
				{
				} 
				newstr = sr.ReadLine();
                massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
				MaxFrameNum = int.Parse(massive[2]);
				frameslider.maxValue = MaxFrameNum;
				frameslider.value = FrameNumber;

				for (int i = 0; i < 4; ++i)
				{	
					sr.ReadLine(); //лишние строки
				}
				
                for (int i=0;i<Number;i++)
                {
                    newstr = sr.ReadLine();
					massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
					masAtomstr[i,0]=massive[0];
					if (feature==true)
					{
						massive[1] = massive[1].Replace('.', ',');
						massive[2] = massive[2].Replace('.', ',');
						massive[3] = massive[3].Replace('.', ',');
					}
						
					masAtomfloat[i,1] = float.Parse(massive[1]);
					masAtomfloat[i,2] = float.Parse(massive[2]);
					masAtomfloat[i,3] = float.Parse(massive[3]);
					
					GameObject newAtom = Instantiate<GameObject>(thePrefab);	
					newAtom.name = massive[0]+"_"+i;
					Vector3 iVector = new Vector3 (masAtomfloat[i,1], masAtomfloat[i,2], masAtomfloat[i,3]);
					newAtom.transform.position = iVector;
					float koef = (((covalentradius[massive[0]]/76.0f)-1.0f)/1.5f)+1.0f; //высчитываем размер атома относительно углерода
					newAtom.transform.localScale = new Vector3(koef, koef, koef);
					newAtom.GetComponent<Renderer>().material.color = atomColor[atomnumber[massive[0]]-1];
					generatedObjects.Add(newAtom);
					//newAtom.GetComponent<BoxCollider>().size=newAtom.transform.localScale;
					findLinks(i, iVector, massive[0]);			
                }
				countLinks();
				drawLinks();
				
				while (sr.ReadLine()!= "IR SPECTRUM")
				{
				}
				for (int i = 0; i < 4; ++i)
				{	
					sr.ReadLine(); //лишние строки
				}
				
				Vector2 circleA = new Vector2(0f,0f);
				Vector2 circleB = new Vector2(0f,0f);
				newstr = sr.ReadLine();
				while (String.IsNullOrEmpty(newstr)!=true)
				{
					massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
					if (feature==true)
					{
						massive[1] = massive[1].Replace('.', ',');
						massive[2] = massive[2].Replace('.', ',');
					}
					
					circleB = new Vector2(float.Parse(massive[1])/8, float.Parse(massive[2])/18);
					circleA = new Vector2(float.Parse(massive[1])/8, 0f);
					ConnectCircle(circleA, circleB);
					newstr = sr.ReadLine();
				}
				float molkoef = 2625.5f;
				while (sr.ReadLine()!= "GIBBS FREE ENERGY")
				{
				}
				for (int i = 0; i < 4; ++i)
				{	
					sr.ReadLine(); //лишние строки
				}
				
				newstr = sr.ReadLine();
				massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
				if (feature==true)
					{
						massive[3] = massive[3].Replace('.', ',');
					}
				float enthalpy = molkoef*float.Parse(massive[3]);
				newstr = sr.ReadLine();
				massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
				if (feature==true)
					{
						massive[4] = massive[4].Replace('.', ',');
					}
				float entropy = molkoef*float.Parse(massive[4]);
				sr.ReadLine();
				newstr = sr.ReadLine();
				massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
				if (feature==true)
					{
						massive[5] = massive[5].Replace('.', ',');
					}
				float energy = molkoef*float.Parse(massive[5]);
				
				Inform2text.GetComponent<TMP_Text>().text = "Общая энтальпия: " + "\n" + enthalpy.ToString() + " kJ/mol" + "\n" + "Поправка на общую энтропию: " + "\n" + entropy.ToString() + " kJ/mol" + "\n" + "Финальная свободная энергия Гиббса: " + "\n" + energy.ToString() + " kJ/mol";
			}
		}
		catch (Exception e)
		{
			Console.WriteLine("The process failed: {0}", e.ToString());
		}	
		return;
	}

	void CreateCircle(Vector2 anchoredPosition)
	{
		GameObject circle = new GameObject("circle", typeof(Image));
		generatedStatistic.Add(circle);
		circle.transform.SetParent(graphContainerStatistic, false);
		circle.GetComponent<Image>().sprite = circleSprite;
		RectTransform rectTransform = circle.GetComponent<RectTransform>();
		rectTransform.anchoredPosition = anchoredPosition;
		rectTransform.sizeDelta = new Vector2(3, 3);
		rectTransform.anchorMin = new Vector2(0, 0);
		rectTransform.anchorMax = new Vector2(0, 0);
	}
	
	void ConnectCircle(Vector2 circleA, Vector2 circleB)
	{
		GameObject circle = new GameObject("circleConnection", typeof(Image));
		generatedObjects.Add(circle);
		circle.transform.SetParent(graphContainerIR, false);
		circle.GetComponent<Image>().sprite = cubeSprite;
		RectTransform rectTransform = circle.GetComponent<RectTransform>();
		rectTransform.anchoredPosition = (circleA+circleB)/2;
		//float angle = (float)(Math.Atan((circleB.y-circleA.y)/(circleB.x-circleA.x))*(180/Math.PI));
		//rectTransform.transform.Rotate(0f, 0f, angle);
		rectTransform.sizeDelta = new Vector2(5, Vector2.Distance(circleA, circleB));
		rectTransform.anchorMin = new Vector2(0, 0);
		rectTransform.anchorMax = new Vector2(0, 0);
	}
	
	void makeGrid(RectTransform graphContainer)
	{
		for (int x = 0; x <= 4000; x=x+500)
		{
				GameObject circle = new GameObject("circleConnection", typeof(Image));
				circle.transform.SetParent(graphContainer, false);
				circle.GetComponent<Image>().sprite = cubeSprite;
				RectTransform rectTransform = circle.GetComponent<RectTransform>();
				rectTransform.anchoredPosition = new Vector2(Mathf.Round(x/8), Mathf.Round(1500/18));
				rectTransform.sizeDelta = new Vector2(2, 3000/18);
				rectTransform.anchorMin = new Vector2(0, 0);
				rectTransform.anchorMax = new Vector2(0, 0);
		}			
		for (int y = 0; y <= 3000; y=y+500)
		{
				GameObject circle = new GameObject("circleConnection", typeof(Image));
				circle.transform.SetParent(graphContainer, false);
				circle.GetComponent<Image>().sprite = cubeSprite;
				RectTransform rectTransform = circle.GetComponent<RectTransform>();
				rectTransform.anchoredPosition = new Vector2(Mathf.Round(2000/8), Mathf.Round(y/18));
				rectTransform.sizeDelta = new Vector2(4000/8, 2);
				rectTransform.anchorMin = new Vector2(0, 0);
				rectTransform.anchorMax = new Vector2(0, 0);
			
		}
		
	}
	
	void findLinks(int i, Vector3 iVector, string type) //создаем цилиндры
	{		
			for (int k=0;k<i;k++)
			{			
				Vector3 kVector = new Vector3 (masAtomfloat[k,1], masAtomfloat[k,2], masAtomfloat[k,3]);
				float dist = Vector3.Distance(iVector, kVector);
	
				if (((covalentradius[type]+covalentradius[masAtomstr[k, 0]])*koefficient)>(dist*100)) //проверяем расстояние
				{
						masLinksfloat[i,0]++; // количество атомов связанных с этим
						masLinksfloat[i,1]++; // количество связей у атома с учетом кратности						
						masLinksfloat[i,3*(int)masLinksfloat[i,0]-1]=k; //номер атома с которым есть связь
						masLinksfloat[i,3*(int)masLinksfloat[i,0]]=dist; //дистанция между атомами
						masLinksfloat[i,3*(int)masLinksfloat[i,0]+1]=1; //кратность связи
						//делаем симметрию
						masLinksfloat[k,0]++; // количество атомов связанных с этим
						masLinksfloat[k,1]++; // количество связей у атома с учетом кратности						
						masLinksfloat[k,3*(int)masLinksfloat[k,0]-1]=i; //номер атома с которым есть связь
						masLinksfloat[k,3*(int)masLinksfloat[k,0]]=dist; //дистанция между атомами
						masLinksfloat[k,3*(int)masLinksfloat[k,0]+1]=1; //кратность связи						
				}						
			}		
			return;
	}
	void countLinks()
	{
		for (int i=0; i<Number; i++) // для каждого атома в масиве
		{
			
			for (int q=1; ((q<=masLinksfloat[i,0])&(masLinksfloat[i,1]<valentnost[atomnumber[masAtomstr[i,0]]-1, 0])); q++) //проверяем атомы, с которыми есть связь, при условии не достижения валентности
			{
						int atomnum = (int)masLinksfloat[i,3*q-1];
						if (masLinksfloat[atomnum,1]<valentnost[atomnumber[masAtomstr[atomnum,0]]-1, 0]) 
						{
							
							masLinksfloat[i,1]++; //изменяем количество связей с учетом кратности у текущего атома
							masLinksfloat[i,3*q+1]++; //изменяем кратность связи между этими атомами у текущего атома
	
							masLinksfloat[atomnum,1]++; //изменяем количество связей с учетом кратности у связанного атома
							for (int r=1; r<=masLinksfloat[atomnum,0]; r++)
							{
								if (masLinksfloat[atomnum,3*r-1]==i)
								{
									masLinksfloat[atomnum,3*r+1]++;
									break;
								}	
							}	
						}	
			}		
		}
	}
	
	void drawLinks() //рисуем цилиндры, i - номер атома
	{	
		GameObject Link = thePrefab;
		for (int k=0;k<Number;k++)
		{
			for (int q=1; (q<=masLinksfloat[k,0]); q++)
			{	
					if (masLinksfloat[k,3*q+1]==1)
						Link = thePrefabSingleLink;
					else if (masLinksfloat[k,3*q+1]==2)
						Link = thePrefabDoubleLink;
					else if (masLinksfloat[k,3*q+1]==3)
						Link = thePrefabTripleLink;
					
					if (GameObject.Find(masAtomstr[(int)masLinksfloat[k,3*q-1],0]+(int)masLinksfloat[k,3*q-1] + masAtomstr[k,0]+k)){}
					else
					{
						GameObject newPlayer = Instantiate<GameObject>(Link);	
						newPlayer.name = masAtomstr[k,0]+k+masAtomstr[(int)masLinksfloat[k,3*q-1],0]+(int)masLinksfloat[k,3*q-1];
						Vector3 kVector = new Vector3 (masAtomfloat[k,1], masAtomfloat[k,2], masAtomfloat[k,3]);
						Vector3 iVector = new Vector3 (masAtomfloat[(int)masLinksfloat[k,3*q-1],1], masAtomfloat[(int)masLinksfloat[k,3*q-1],2], masAtomfloat[(int)masLinksfloat[k,3*q-1],3]);
						newPlayer.transform.position = (iVector+kVector)/2;
						transform.localScale = new Vector3(1f, 1f, Vector3.Distance(iVector, kVector));					
						newPlayer.transform.LookAt(iVector);
						generatedObjects.Add(newPlayer);
					}
			}
		}
		return;
	}
	
	void updateLinks() //перемещаем цилиндры,
	{	
		for (int k=0;k<Number;k++)
		{
			for (int q=1; (q<=masLinksfloat[k,0]); q++)
			{	
					if (GameObject.Find(masAtomstr[(int)masLinksfloat[k,3*q-1],0]+(int)masLinksfloat[k,3*q-1] + masAtomstr[k,0]+k)){}
					else
					{
						string linkname = masAtomstr[k,0]+k+masAtomstr[(int)masLinksfloat[k,3*q-1],0]+(int)masLinksfloat[k,3*q-1];
						GameObject newPlayer = GameObject.Find(linkname);
						Vector3 kVector = new Vector3 (masAtomfloat[k,1], masAtomfloat[k,2], masAtomfloat[k,3]);
						Vector3 iVector = new Vector3 (masAtomfloat[(int)masLinksfloat[k,3*q-1],1], masAtomfloat[(int)masLinksfloat[k,3*q-1],2], masAtomfloat[(int)masLinksfloat[k,3*q-1],3]);
						newPlayer.transform.position = (iVector+kVector)/2;					
						newPlayer.transform.LookAt(iVector);
					}
			}
		}
		if (selected > 1)
		{
			generatedCylinder[0].transform.position = (selectedObjects[0].transform.position+selectedObjects[1].transform.position)/2;
			generatedCylinder[0].transform.LookAt(selectedObjects[1].transform.position);
			generatedCylinder[0].transform.localScale = new Vector3(3f, 3f, Vector3.Distance(selectedObjects[0].transform.position, selectedObjects[1].transform.position));
			if (selected > 2)
			{	
				generatedCylinder[1].transform.position = (selectedObjects[1].transform.position+selectedObjects[2].transform.position)/2;
				generatedCylinder[1].transform.LookAt(selectedObjects[2].transform.position);
				generatedCylinder[1].transform.localScale = new Vector3(3f, 3f, Vector3.Distance(selectedObjects[1].transform.position, selectedObjects[2].transform.position));
				if (selected == 6)
				{
					generatedCylinder[2].transform.position = (selectedObjects[3].transform.position+selectedObjects[4].transform.position)/2;
					generatedCylinder[2].transform.LookAt(selectedObjects[4].transform.position);
					generatedCylinder[2].transform.localScale = new Vector3(3f, 3f, Vector3.Distance(selectedObjects[3].transform.position, selectedObjects[4].transform.position));
					generatedCylinder[3].transform.position = (selectedObjects[4].transform.position+selectedObjects[5].transform.position)/2;
					generatedCylinder[3].transform.LookAt(selectedObjects[5].transform.position);
					generatedCylinder[3].transform.localScale = new Vector3(3f, 3f, Vector3.Distance(selectedObjects[4].transform.position, selectedObjects[5].transform.position));
				}
			}
		}
		return;
	}
	
	void readFrameXYZ () //здесь мы только перемещаем атомы и цилиндры
	{
				
		try
		{	
			
			using (StreamReader sr = new StreamReader(PathF))
			{
							
				sr.ReadLine();
				int linesToSkip = ((Number+2)*(FrameNumber-1));
                for (int i = 0; i <= linesToSkip; ++i)
				{	
					sr.ReadLine(); //лишние строки
				}					
                for (int i=0;i<Number;i++)
                {

					string newstr = sr.ReadLine();
                    string[] massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
					if (feature==true)
					{
						massive[1] = massive[1].Replace('.', ',');
						massive[2] = massive[2].Replace('.', ',');
						massive[3] = massive[3].Replace('.', ',');
					} 
					
					masAtomfloat[i,1] = float.Parse(massive[1]);
					masAtomfloat[i,2] = float.Parse(massive[2]);
					masAtomfloat[i,3] = float.Parse(massive[3]);
					
					GameObject newPlayer = GameObject.Find(massive[0]+"_"+i);
					Vector3 iVector = new Vector3 (masAtomfloat[i,1], masAtomfloat[i,2], masAtomfloat[i,3]);
					newPlayer.transform.position = iVector;	
                }
				updateLinks();
			}	
		}
		catch (Exception e)
		{
            Console.WriteLine("The process failed: {0}", e.ToString());
		}
        UpdateInform();   
	}
	
	void UpdateInform()
	{
		if (lastselObj!=thePrefab)
			Inform.GetComponent<TMP_Text>().text = "Атом:" + '\n' + lastselObj.name + '\n' + " Координаты:" + '\n' + lastselObj.transform.position;
		else 
			Inform.GetComponent<TMP_Text>().text = "Выберите атом";
		if (selected == 1)
		{	
			InformChoosen.GetComponent<TMP_Text>().text = "Атом:" + '\n' + selectedObjects[0].name + '\n' + "Кооридинаты:" + '\n' + selectedObjects[0].transform.position;	
		}
		else if (selected == 2)
		{
			InformChoosen.GetComponent<TMP_Text>().text = "Атомы:" + '\n' + selectedObjects[0].name + ' ' + selectedObjects[1].name + '\n' + "Расстояние:" + '\n' + Vector3.Distance(selectedObjects[0].transform.position, selectedObjects[1].transform.position);
		}
		else if (selected == 3)
		{	float angl = Vector3.Angle((selectedObjects[0].transform.position - selectedObjects[1].transform.position), (selectedObjects[2].transform.position - selectedObjects[1].transform.position));
			InformChoosen.GetComponent<TMP_Text>().text = "Атомы:" + '\n' + selectedObjects[0].name + ' ' + selectedObjects[1].name + ' ' + selectedObjects[2].name + '\n' + "Угол:" + '\n' + angl;
		}
		else if ((selected == 5) || (selected == 4))
		{	
			InformChoosen.GetComponent<TMP_Text>().text = "Выберите атомы для второй плоскости";
		}
		else if (selected == 6)
		{
			Vector3 plane1 = Vector3.Cross((selectedObjects[0].transform.position - selectedObjects[1].transform.position), (selectedObjects[2].transform.position - selectedObjects[1].transform.position));
			Vector3 plane2 = Vector3.Cross((selectedObjects[3].transform.position - selectedObjects[4].transform.position), (selectedObjects[5].transform.position - selectedObjects[4].transform.position));
			float angle2 = Vector3.Angle(plane1, plane2);
			InformChoosen.GetComponent<TMP_Text>().text = "Первая плоскость:" + '\n' + selectedObjects[0].name + ' ' + selectedObjects[1].name + ' ' + selectedObjects[2].name + '\n' + "Вторая плоскость:" + '\n' + selectedObjects[3].name + ' ' + selectedObjects[4].name + ' ' + selectedObjects[5].name + '\n' + "Двугранный угол:" + '\n' + angle2;
		}
	}
	
	void readFrameOUT() //здесь мы только перемещаем атомы и цилиндры
	{		
		try
		{			
			using (StreamReader sr = new StreamReader(PathF))
			{
				for (int i = 0; i < FrameNumber; i++)
				{
					while (sr.ReadLine()!= "CARTESIAN COORDINATES (ANGSTROEM)")
					{
					}
				}	
				sr.ReadLine(); //лишние строки
				
                for (int i=0;i<Number;i++)
                {
					string newstr = sr.ReadLine();
                    string[] massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
					if (feature==true)
					{
						massive[1] = massive[1].Replace('.', ',');
						massive[2] = massive[2].Replace('.', ',');
						massive[3] = massive[3].Replace('.', ',');
					} 
					
					masAtomfloat[i,1] = float.Parse(massive[1]);
					masAtomfloat[i,2] = float.Parse(massive[2]);
					masAtomfloat[i,3] = float.Parse(massive[3]);
					
					GameObject newPlayer = GameObject.Find(massive[0]+"_"+i);
					Vector3 iVector = new Vector3 (masAtomfloat[i,1], masAtomfloat[i,2], masAtomfloat[i,3]);
					newPlayer.transform.position = iVector;							
                }
				updateLinks();
			}	
		}
		catch (Exception e)
		{
            Console.WriteLine("The process failed: {0}", e.ToString());
		}
        UpdateInform();   
	}
	
	public void TotalDestroyer() //удаляем все атомы и цилиндры
	{
		lastselObj = thePrefab;
		selectedObjects = new List<GameObject>();
		selected = 0;
		selectedatom.text = "0";
		lastselectedatom.text = "0";
		MatNorm = null;
		foreach(var obj in generatedObjects)
		{
			Destroy(obj);
		}
		generatedObjects = new List<GameObject>();
		FrameNumber=1;
		InformChoosen.GetComponent<TMP_Text>().text = "Выберите атом";
		Inform.GetComponent<TMP_Text>().text = "Текущая информация";
		foreach(var obj in generatedCylinder)
		{
			Destroy(obj);
		}
		generatedCylinder = new List<GameObject>();
	}
	
	public void Downloading() 
	{
        OpenFileName openFileName = new OpenFileName();
        if (LocalDialog.GetOpenFileName(openFileName))
        {
            Debug.Log(openFileName.file);
            Debug.Log(openFileName.fileTitle);
			PathF = openFileName.file;
			string[] massive = openFileName.fileTitle.Split(".", StringSplitOptions.RemoveEmptyEntries);
			TotalDestroyer();
			MaxFrameNum = 1;
			if (massive[1] == "out")
			{
				formatfile = 1;
				readFirstFrameOUT();
			}
			else if (massive[1] == "xyz")
			{
				
				try
				{
					using (StreamReader r = new StreamReader(PathF))
					{
						int i = 1;
						string n = r.ReadLine();
						int num = int.Parse(n);
						while (r.ReadLine() != null) { i++; }
						MaxFrameNum = i/(num+2);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine("The process failed: {0}", e.ToString());
				}
				formatfile = 2;
				readFirstFrameXYZ();
			}
			else return;
			frameslider.maxValue = MaxFrameNum;
			frameslider.value = FrameNumber;
			buttonMD.SetActive(false);
			buttonQ.SetActive(false);
        }
    }
	
	public void OnButtonDownXYZ(int x) //включить файл xyz из библиотеки 
    {
		if (x==1)
		{
			FileName = "M-BA-1000fr.xyz";
		}
		else if (x==2)
		{
			FileName = "M-CA-1000fr.xyz";
		}	
		else if (x==3)
		{
			FileName = "GLU-FRU-1000fr.xyz";
		}	
		else if (x==4)
		{
			FileName = "M-BA1-1000fr.xyz";
		}
		variant = x;
		string tempPath = System.IO.Path.Combine(Application.streamingAssetsPath, FileName);
		WWW reader = new WWW(tempPath);
		while ( ! reader.isDone) {}
		PathF = Application.persistentDataPath + "/db/" + FileName;
		TotalDestroyer();
		MaxFrameNum=1000;
		frameslider.maxValue = MaxFrameNum;
		frameslider.value = FrameNumber;
		readFirstFrameXYZ();
		buttonMD.SetActive(true);
		formatfile = 2;	       
    }
	
	public void OnButtonDownOUT(int x) //включить файл out из библиотеки 
    { 
		if (x == 1)
		{	
			FileName = "MEL-BA.out";
			variant = 1;
		}
		else if (x==2)
		{	
			FileName = "CA-MEL.out";
			variant = 2;
		}	
		else if (x==3)
		{	
			FileName = "Glu-Fru.out";
			variant = 3;
		}	
		else if (x==4)
		{	
			FileName = "M-BA1.out";
			variant = 4;
		}	
		else if (x==5)
		{	
			FileName = "BA.out"; 
			variant = 1;
		}
		else if (x==6)
		{	
			FileName = "CA.out"; 
			variant = 2;
		}	
		else if (x==7)
		{	
			FileName = "MEL.out";
			if (variant == 3)
				variant = 1;
		}	
		else if (x==8)
		{	
			FileName = "GLU.out"; 
			variant = 3;
		}
		else if (x==9)
		{	
			FileName = "FRU.out";
			variant = 3;
		}	
		else if (x==10)
		{	
			FileName = "BA1.out";
			variant = 4;
		}			
		string tempPath = System.IO.Path.Combine(Application.streamingAssetsPath, FileName);
		WWW reader = new WWW(tempPath);
		while ( ! reader.isDone) {}
		PathF = Application.persistentDataPath + "/db/" + FileName;
		TotalDestroyer();
		MaxFrameNum=1;
		frameslider.maxValue = MaxFrameNum;
		frameslider.value = FrameNumber;
		readFirstFrameOUT();
		formatfile = 1;
		buttonQ.SetActive(true);
    }
	
	public void OnButtonExit()
	{
		Application.Quit();
	}
	
	public void Count(object obj)
	{
		FrameNumber++;
		if (FrameNumber>MaxFrameNum) 
		{	
			FrameNumber = 1;
		}
		frameslider.value = FrameNumber;
		if (formatfile == 2)
			readFrameXYZ();
		else readFrameOUT();
		framenumber.text = FrameNumber.ToString();
	}
	
	public void OnButtonFrameNext() //кнопка "следующий фрейм"
	{
		FrameNumber++;
		if (FrameNumber>MaxFrameNum) 
		{	
			FrameNumber = 1;
		}
		frameslider.value = FrameNumber;
		if (formatfile == 2)
			readFrameXYZ();
		else readFrameOUT();
		framenumber.text = FrameNumber.ToString();
	}
	
	public void OnButtonFramePrevious() //кнопка "предыдущий фрейм"
	{
		FrameNumber=FrameNumber-1;
		if (FrameNumber<1) 
		{	
			FrameNumber = MaxFrameNum;
		}
		frameslider.value = FrameNumber;
		if (formatfile == 2)
			readFrameXYZ();
		else readFrameOUT();
		framenumber.text = FrameNumber.ToString();
	}
	
	public void fixdot ()
	{
		if (feature == true)
		{
			feature = false;
		}
		else
		{
			feature = true;
		}
		TotalDestroyer();
		frameslider.value = FrameNumber;
		if (formatfile == 2) 
			readFirstFrameXYZ();
		else readFirstFrameOUT();
	}
	
	public void ButtonNewView() //меняем вид атомов
	{
		if (ViewAtom==0)
		{
			foreach(var obj in generatedObjects)
			{
			ChangeMat(obj,2);
			}
			ViewAtom=1;
		}
		else 
		{
			foreach(var obj in generatedObjects)
			{
			ChangeMat(obj,3);
			}
			ViewAtom=0;
		}
		
		foreach(var obj in selectedObjects)
		{
			ChangeMat(obj, 0);
		}	
	}
	
	public void ButtonNewSky() //меняем фон
	{
		if (SkyBox==0)
		{
			classbox.SetActive(false);
			RenderSettings.skybox = SkyC;
			RenderSettings.fogColor= new Color(0F, 0F, 0F);
			SkyBox++;
		}
		else if (SkyBox==1)
		{
			RenderSettings.skybox = SkyR;
			RenderSettings.fogColor= new Color(0F, 0F, 0F);
			SkyBox++;
		}
		else if (SkyBox==2)
		{
			RenderSettings.skybox = SkyBlack;
			RenderSettings.fogColor= new Color(0F, 0F, 0F);
			SkyBox++;
		}
		else if (SkyBox==3)
		{
			RenderSettings.skybox = SkyGrey;
			RenderSettings.fogColor= new Color(0F, 0F, 0F);
			SkyBox++;
		}
		else if (SkyBox==4)
		{
			RenderSettings.skybox = SkyWhite;
			RenderSettings.fogColor= new Color(0F, 0F, 0F);
			SkyBox++;
		}
		else if (SkyBox==5)
		{
			RenderSettings.skybox = SkyB;
			RenderSettings.fogColor= new Color(0.5F, 0.7F, 0.8F);
			SkyBox++;
		}	
		else	
		{
			classbox.SetActive(true);
			RenderSettings.skybox = SkyC;
			RenderSettings.fogColor= new Color(0F, 0F, 0F);
			SkyBox=0;
		}
	}
	
	public void CloseGraph(int i)
	{
		if (i == 0)
			Statistic.SetActive(false);	
		else 
			IRspectrum.gameObject.SetActive(false);
	}
	
	public void Showspectrum()
	{
		IRspectrum.gameObject.SetActive(true);
	}
	
	public void ShowStatistic() //обновляем статистику
	{
		foreach(var obj in generatedStatistic)
		{
			Destroy(obj);
		}
		generatedStatistic = new List<GameObject>();
		//Statistic.SetActive(true);	
		try
		{	
			using (StreamReader sr = new StreamReader(PathF))
			{
				string key;
				if (formatfile == 1)
				{
					key = "CARTESIAN COORDINATES (ANGSTROEM)";
				}
				else
				{	
					key = Number.ToString();
				}
				Debug.Log(key);
				if (selected == 2)
				{
					textforgraph.GetComponent<TMP_Text>().text = "Расстояние";
					string[] massive = selectedObjects[0].name.Split("_", StringSplitOptions.RemoveEmptyEntries);
					int sel_1 = int.Parse(massive[1]);
					massive = selectedObjects[1].name.Split("_", StringSplitOptions.RemoveEmptyEntries);
					int sel_2 = int.Parse(massive[1]);
					for (int i = 0; i < MaxFrameNum; i++)
					{
						while (sr.ReadLine()!= key)
						{}
						sr.ReadLine();
						int n;
						for (n = 0; ((n != sel_1)&(n!=sel_2)); n++)
						{
							sr.ReadLine();//лишние строки
						}
						string newstr = sr.ReadLine();
						massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
						if (feature==true)
						{
							massive[1] = massive[1].Replace('.', ',');
							massive[2] = massive[2].Replace('.', ',');
							massive[3] = massive[3].Replace('.', ',');
						}
						Vector3 v_sel_1 = new Vector3(float.Parse(massive[1]), float.Parse(massive[2]), float.Parse(massive[3]));
						
						for (n++; ((n != sel_1)&(n!=sel_2)); n++)
						{
							sr.ReadLine();//лишние строки
						}
						newstr=sr.ReadLine();
						massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
						if (feature==true)
						{
							massive[1] = massive[1].Replace('.', ',');
							massive[2] = massive[2].Replace('.', ',');
							massive[3] = massive[3].Replace('.', ',');
						}
						Vector3 v_sel_2 = new Vector3(float.Parse(massive[1]), float.Parse(massive[2]), float.Parse(massive[3]));
						CreateCircle(new Vector2(i/2, Vector3.Distance(v_sel_1, v_sel_2)));	
					}	
				}
				else if (selected == 3)
				{	
					textforgraph.GetComponent<TMP_Text>().text = "Угол";
					string[] massive = selectedObjects[0].name.Split("_", StringSplitOptions.RemoveEmptyEntries);
					int sel_1 = int.Parse(massive[1]);
					massive = selectedObjects[1].name.Split("_", StringSplitOptions.RemoveEmptyEntries);
					int sel_2 = int.Parse(massive[1]);
					massive = selectedObjects[2].name.Split("_", StringSplitOptions.RemoveEmptyEntries);
					int sel_3 = int.Parse(massive[1]);
					for (int i = 0; i < MaxFrameNum; i++)
					{
						while (sr.ReadLine()!= key)
						{
						}
						sr.ReadLine();
						Vector3 v_sel_1 = new Vector3(0, 0, 0);
						Vector3 v_sel_2 = new Vector3(0, 0, 0);
						Vector3 v_sel_3 = new Vector3(0, 0, 0);
						for (int n = 0; n < Number; n++)
						{
							if (n == sel_1)
							{
								string newstr = sr.ReadLine();
								massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
								if (feature==true)
								{
									massive[1] = massive[1].Replace('.', ',');
									massive[2] = massive[2].Replace('.', ',');
									massive[3] = massive[3].Replace('.', ',');
								}
								v_sel_1 = new Vector3(float.Parse(massive[1]), float.Parse(massive[2]), float.Parse(massive[3]));
							}
							else if (n == sel_2)
							{
								string newstr=sr.ReadLine();
								massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
								if (feature==true)
								{
									massive[1] = massive[1].Replace('.', ',');
									massive[2] = massive[2].Replace('.', ',');
									massive[3] = massive[3].Replace('.', ',');
								}
								v_sel_2 = new Vector3(float.Parse(massive[1]), float.Parse(massive[2]), float.Parse(massive[3]));
							}
							else if (n == sel_3)
							{
								string newstr=sr.ReadLine();
								massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
								if (feature==true)
								{
									massive[1] = massive[1].Replace('.', ',');
									massive[2] = massive[2].Replace('.', ',');
									massive[3] = massive[3].Replace('.', ',');
								}
								v_sel_3 = new Vector3(float.Parse(massive[1]), float.Parse(massive[2]), float.Parse(massive[3]));
							}
							else
								sr.ReadLine();//лишние строки
						}
						float angl = Vector3.Angle((v_sel_1 - v_sel_2), (v_sel_3 - v_sel_2));
						CreateCircle(new Vector2(i/2, angl));
					}	
				}	
				else if (selected == 6)
				{
					textforgraph.GetComponent<TMP_Text>().text = "Двугранный угол";
					string[] massive = selectedObjects[0].name.Split("_", StringSplitOptions.RemoveEmptyEntries);
					int sel_1 = int.Parse(massive[1]);
					massive = selectedObjects[1].name.Split("_", StringSplitOptions.RemoveEmptyEntries);
					int sel_2 = int.Parse(massive[1]);
					massive = selectedObjects[2].name.Split("_", StringSplitOptions.RemoveEmptyEntries);
					int sel_3 = int.Parse(massive[1]);
					massive = selectedObjects[3].name.Split("_", StringSplitOptions.RemoveEmptyEntries);
					int sel_4 = int.Parse(massive[1]);
					massive = selectedObjects[4].name.Split("_", StringSplitOptions.RemoveEmptyEntries);
					int sel_5 = int.Parse(massive[1]);
					massive = selectedObjects[5].name.Split("_", StringSplitOptions.RemoveEmptyEntries);
					int sel_6 = int.Parse(massive[1]);
					for (int i = 0; i < MaxFrameNum; i++)
					{
						while (sr.ReadLine()!= key)
						{
						}
						sr.ReadLine();
						Vector3 v_sel_1 = new Vector3(0, 0, 0);
						Vector3 v_sel_2 = new Vector3(0, 0, 0);
						Vector3 v_sel_3 = new Vector3(0, 0, 0);
						Vector3 v_sel_4 = new Vector3(0, 0, 0);
						Vector3 v_sel_5 = new Vector3(0, 0, 0);
						Vector3 v_sel_6 = new Vector3(0, 0, 0);
						for (int n = 0; n < Number; n++)
						{
							if (n == sel_1)
							{
								string newstr = sr.ReadLine();
								massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
								if (feature==true)
								{
									massive[1] = massive[1].Replace('.', ',');
									massive[2] = massive[2].Replace('.', ',');
									massive[3] = massive[3].Replace('.', ',');
								}
								v_sel_1 = new Vector3(float.Parse(massive[1]), float.Parse(massive[2]), float.Parse(massive[3]));
							}
							else if (n == sel_2)
							{
								string newstr=sr.ReadLine();
								massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
								if (feature==true)
								{
									massive[1] = massive[1].Replace('.', ',');
									massive[2] = massive[2].Replace('.', ',');
									massive[3] = massive[3].Replace('.', ',');
								}
								v_sel_2 = new Vector3(float.Parse(massive[1]), float.Parse(massive[2]), float.Parse(massive[3]));
							}
							else if (n == sel_3)
							{
								string newstr=sr.ReadLine();
								massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
								if (feature==true)
								{
									massive[1] = massive[1].Replace('.', ',');
									massive[2] = massive[2].Replace('.', ',');
									massive[3] = massive[3].Replace('.', ',');
								}
								v_sel_3 = new Vector3(float.Parse(massive[1]), float.Parse(massive[2]), float.Parse(massive[3]));
							}
							else if (n == sel_4)
							{
								string newstr=sr.ReadLine();
								massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
								if (feature==true)
								{
									massive[1] = massive[1].Replace('.', ',');
									massive[2] = massive[2].Replace('.', ',');
									massive[3] = massive[3].Replace('.', ',');
								}
								v_sel_4 = new Vector3(float.Parse(massive[1]), float.Parse(massive[2]), float.Parse(massive[3]));
							}
							else if (n == sel_5)
							{
								string newstr=sr.ReadLine();
								massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
								if (feature==true)
								{
									massive[1] = massive[1].Replace('.', ',');
									massive[2] = massive[2].Replace('.', ',');
									massive[3] = massive[3].Replace('.', ',');
								}
								v_sel_5 = new Vector3(float.Parse(massive[1]), float.Parse(massive[2]), float.Parse(massive[3]));
							}
							else if (n == sel_6)
							{
								string newstr=sr.ReadLine();
								massive = newstr.Split(" ", StringSplitOptions.RemoveEmptyEntries);
								if (feature==true)
								{
									massive[1] = massive[1].Replace('.', ',');
									massive[2] = massive[2].Replace('.', ',');
									massive[3] = massive[3].Replace('.', ',');
								}
								v_sel_6 = new Vector3(float.Parse(massive[1]), float.Parse(massive[2]), float.Parse(massive[3]));
							}
							else 
								sr.ReadLine();
						}	
						Vector3 plane1 = Vector3.Cross((v_sel_1 - v_sel_2), (v_sel_3 - v_sel_2));
						Vector3 plane2 = Vector3.Cross((v_sel_4 - v_sel_5), (v_sel_6 - v_sel_5));
						float angle2 = Vector3.Angle(plane1, plane2);
						CreateCircle(new Vector2(i/2, angle2));
					}
				}
			}	
		}
		catch (Exception e)
		{
            Console.WriteLine("The process failed: {0}", e.ToString());
		}
		return;
	}
	
     void Update()
    {
		if ((int)frameslider.value != FrameNumber) //слайдер для фреймов
		{
			FrameNumber = (int)frameslider.value;
			if (formatfile == 2) 
				readFrameXYZ();
			else 
				readFrameOUT();
		}
		framenumber.text = FrameNumber.ToString()+'/'+ MaxFrameNum.ToString();
		
	//ниже скрипт для выделения	атомов				
            if (GameObject.Find(lastselectedatom.text))
			{
				GameObject myObj = GameObject.Find(lastselectedatom.text);
				
				if (myObj!=lastselObj) 
				{
					lastselObj.GetComponent<Renderer>().material = MatNorm;
					
					MatNorm = myObj.GetComponent<Renderer>().material;
					ChangeMat(myObj, 1);
					lastselObj=myObj;
				}
				
				Inform.GetComponent<TMP_Text>().text = "Атом: " + myObj.name + "  Координаты: " + myObj.transform.position; // показывает информацию об атоме
	
				if (selected != int.Parse(selectedatom.text))
				{
					if (selected <= 6)
					{
						selected++;
						selectedObjects.Add(myObj);
						ChangeMat(myObj, 0);
						MatNorm = myObj.GetComponent<Renderer>().material;
					}
					if (selected == 1)
					{	
						InformChoosen.GetComponent<TMP_Text>().text = "Атом:" + '\n' + selectedObjects[0].name + '\n' + "Координаты:" + '\n' + selectedObjects[0].transform.position;
					}
					else if (selected == 2)
					{
						InformChoosen.GetComponent<TMP_Text>().text = "Атомы:" + '\n' + selectedObjects[0].name + ' ' + selectedObjects[1].name + '\n' + "Расстояние:" + '\n' + Vector3.Distance(selectedObjects[0].transform.position, selectedObjects[1].transform.position);
						GameObject newPlayer = Instantiate<GameObject>(ruller);	
						newPlayer.name = "ruller";
						newPlayer.transform.position = (selectedObjects[0].transform.position+selectedObjects[1].transform.position)/2;
						newPlayer.transform.LookAt(selectedObjects[1].transform.position);
						newPlayer.transform.localScale = new Vector3(3f, 3f, Vector3.Distance(selectedObjects[0].transform.position, selectedObjects[1].transform.position));
						generatedCylinder.Add(newPlayer);
						ShowStatistic();
					}
					else if (selected == 3)
					{	
						float angl = Vector3.Angle((selectedObjects[0].transform.position - selectedObjects[1].transform.position), (selectedObjects[2].transform.position - selectedObjects[1].transform.position));
						InformChoosen.GetComponent<TMP_Text>().text = "Атомы:" + '\n' + selectedObjects[0].name + ' ' + selectedObjects[1].name + ' ' + selectedObjects[2].name + '\n' + "Угол:" + '\n' + angl;
						GameObject newPlayer = Instantiate<GameObject>(ruller);	
						newPlayer.name = "ruller";
						newPlayer.transform.position = (selectedObjects[1].transform.position+selectedObjects[2].transform.position)/2;
						newPlayer.transform.LookAt(selectedObjects[2].transform.position);
						newPlayer.transform.localScale = new Vector3(3f, 3f, Vector3.Distance(selectedObjects[1].transform.position, selectedObjects[2].transform.position));
						generatedCylinder.Add(newPlayer);
						ShowStatistic();
					}
					else if ((selected == 4) || (selected == 5))
					{	
						InformChoosen.GetComponent<TMP_Text>().text = "Выберите атомы для второй плоскости";
					}
					else if (selected == 6)
					{
						Vector3 plane1 = Vector3.Cross((selectedObjects[0].transform.position - selectedObjects[1].transform.position), (selectedObjects[2].transform.position - selectedObjects[1].transform.position));
						Vector3 plane2 = Vector3.Cross((selectedObjects[3].transform.position - selectedObjects[4].transform.position), (selectedObjects[5].transform.position - selectedObjects[4].transform.position));
						float angle2 = Vector3.Angle(plane1, plane2);
						InformChoosen.GetComponent<TMP_Text>().text = "Первая плоскость:" + '\n' + selectedObjects[0].name + ' ' + selectedObjects[1].name + ' ' + selectedObjects[2].name + '\n' + "Вторая плоскость:" + '\n' + selectedObjects[3].name + ' ' + selectedObjects[4].name + ' ' + selectedObjects[5].name + '\n' + "Двугранный угол:" + '\n' + angle2;
						GameObject newPlayer = Instantiate<GameObject>(ruller);	
						newPlayer.name = "ruller";
						newPlayer.transform.position = (selectedObjects[3].transform.position+selectedObjects[4].transform.position)/2;
						newPlayer.transform.LookAt(selectedObjects[4].transform.position);
						newPlayer.transform.localScale = new Vector3(3f, 3f, Vector3.Distance(selectedObjects[3].transform.position, selectedObjects[4].transform.position));
						generatedCylinder.Add(newPlayer);
						GameObject newPlayer2 = Instantiate<GameObject>(ruller);	
						newPlayer2.name = "ruller";
						newPlayer2.transform.position = (selectedObjects[4].transform.position+selectedObjects[5].transform.position)/2;
						newPlayer2.transform.LookAt(selectedObjects[5].transform.position);
						newPlayer2.transform.localScale = new Vector3(3f, 3f, Vector3.Distance(selectedObjects[4].transform.position, selectedObjects[5].transform.position));
						generatedCylinder.Add(newPlayer2);
						ShowStatistic();
						
					}
					else 
					{
						clearselection();
					} 
				}
			}
        
	}
		
	public void SwapQMD(int i) // переключение между квантами и группами молекул
	{
		if (i == 1) // включаем кванты
		{
			Inform2.SetActive(true);			
			OnButtonDownOUT(variant);
			buttonquant.SetActive(true);
			buttonQ.SetActive(true);
			if (Statistic.activeSelf)
			{
				IRspectrum.gameObject.SetActive(true);
			}
		}
		else //включаем мд
		{
			Inform2.SetActive(false);
			buttonquant.SetActive(false);
			OnButtonDownXYZ(variant);
			IRspectrum.gameObject.SetActive(false);
			buttonQ.SetActive(false);			
		}
		formatfile = i;
	}	
	
	public void setvariant(int x)
	{
		variant = x;
		if (formatfile == 1)
		{
			OnButtonDownOUT(variant);
		}
		else
		{
			OnButtonDownXYZ(variant);
		}
	}
		
	public void clearselection()
	{	
		lastselObj.GetComponent<Renderer>().material = MatNorm;
		lastselObj=thePrefab;
		selectedatom.text = "0";
		lastselectedatom.text = "0";
		if (ViewAtom==0)
		{
			foreach(var obj in selectedObjects)
			{
				ChangeMat(obj,2);
			}	
		}
		else 
			foreach(var obj in selectedObjects)
			{
				ChangeMat(obj,3);
			}	
		selectedObjects = new List<GameObject>();
		selected = 0;
		MatNorm = null;
		InformChoosen.GetComponent<TMP_Text>().text = "Выберите атом";
		foreach(var obj in generatedCylinder)
		{
			Destroy(obj);
		}
		generatedCylinder = new List<GameObject>();
	}
	
	public void ChangeMat(GameObject myObj, int i)
	{
		string atom = myObj.name;
		if (i==0)
		{
			if (atom[0]=='N')
			{
				myObj.GetComponent<Renderer>().material = FBlue2;
			}
			else if (atom[0]=='C')
			{
				myObj.GetComponent<Renderer>().material = FBlack2;
			}
			else if (atom[0]=='H')
			{
				myObj.GetComponent<Renderer>().material = FWhite2;
			}
			else if (atom[0]=='O')
			{
				myObj.GetComponent<Renderer>().material = FRed2;
			}
		}
		else if (i==1)
		{
			if (atom[0]=='N')
			{
				myObj.GetComponent<Renderer>().material = FBlue1;
			}
			else if (atom[0]=='C')
			{
				myObj.GetComponent<Renderer>().material = FBlack1;
			}
			else if (atom[0]=='H')
			{
				myObj.GetComponent<Renderer>().material = FWhite1;
			}
			else if (atom[0]=='O')
			{
				myObj.GetComponent<Renderer>().material = FRed1;
			}
		}
		else if (i==2)
			{
				if (atom[0]=='N')
				{
				myObj.GetComponent<Renderer>().material = FBlue;
				}
				else if (atom[0]=='C')
				{
					myObj.GetComponent<Renderer>().material = FBlack;
				}
				else if (atom[0]=='H')
				{
					myObj.GetComponent<Renderer>().material = FWhite;
				}
				else if (atom[0]=='O')
				{
					myObj.GetComponent<Renderer>().material = FRed;
				}
			}
			else 
			{
				if (atom[0]=='N')
				{
					myObj.GetComponent<Renderer>().material = Blue;
				}
				else if (atom[0]=='C')
				{
					myObj.GetComponent<Renderer>().material = Black;
				}
				else if (atom[0]=='H')
				{
					myObj.GetComponent<Renderer>().material = White;
				}
				else if (atom[0]=='O')
				{
					myObj.GetComponent<Renderer>().material = Red;
				}
			}
		
	}

	public void FileInformation()
	{
		FileInform.GetComponent<TMP_Text>().text = " Файл: " + FileName + "\n" + " Количество атомов: " + Number.ToString() + "\n" + " Числo фреймов: " + MaxFrameNum;
	}
	
	public void MakeNotion()
	{
		try
		{			
			File.AppendAllText(Application.persistentDataPath + RecordF, "\n" + "Вариант " + variant.ToString() + "\n" + InformChoosen.GetComponent<TMP_Text>().text);
			Inform.GetComponent<TMP_Text>().text = "Информация сохранена";
		}
		catch (Exception e)
		{
            Console.WriteLine("The process failed: {0}", e.ToString());
		}
	}
	
	public void SetBoxColliderSize()
	{
		Vector3 point_A = mainCamera.ScreenPointToRay(Vector2.zero).origin;

		// определяем размер коллайдера по ширине экрана
		Vector3 point_B = mainCamera.ScreenPointToRay(new Vector2(Screen.width, 0)).origin;

		float dist = Vector3.Distance(point_A, point_B);
		boxCollider.size = new Vector3(dist, boxCollider.size.y, 0.1f);

		// определяем размер бокса по высоте
		point_B = mainCamera.ScreenPointToRay(new Vector2(0, Screen.height)).origin;

		dist = Vector3.Distance(point_A, point_B);
		boxCollider.size = new Vector3(boxCollider.size.x, dist, 0.1f);

		boxCollider.center = new Vector3(0, 0, mainCamera.nearClipPlane);
	}
}


[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenFileName
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
    public OpenFileName(int FileLenth = 256, int FileTitleLenth=64)
    {
        structSize=Marshal.SizeOf(this);
        file = new string(new char[FileLenth]);
        maxFile = file.Length;
        fileTitle = new string(new char[FileTitleLenth]);
        maxFileTitle = fileTitle.Length;
        title = String.Empty;
        flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
        title = "Download file";
        initialDir = Application.streamingAssetsPath.Replace('/', '\\');
    }
}

public class LocalDialog
{
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetSaveFileName([In, Out] OpenFileName ofn);
}
