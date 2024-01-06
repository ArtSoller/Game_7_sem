using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp2;

public class Movements(Page page)
{
    private readonly Page _myPage = page;

    protected DispatcherTimer? gameTimer;

    protected bool _isUpKeyPressed, _isDownKeyPressed, _isLeftKeyPressed, _isRightKeyPressed;

    protected const float _friction = 0.88F, _speed = 0.8F;

    protected float _speedX, _speedY;

    protected bool _isPossibleUpwardMovement, _isPossibleDownwardMovement,
                 _isPossibleLeftwardMovement, _isPossibleRightwardMovement;

    protected bool _isPlayerMovingUpward, _isPlayerMovingLeftward,
                 _isPlayerMovingRightward, _isPlayerMovingDownward;



}
